'
' $Id: SqliteAmplificationCaseRepository.vb,v 6.12 2011/04/03 02:14:08 dbadmin Exp $
' $Source: /CorporateSystems/BLOAT/Data/Embedded/CaseRepository.vb $
'
' EMBEDDED DATABASE DEPLOYMENT NOTICE:
'
' SQLite was selected following an eighteen-month database platform
' evaluation because it was already installed and nobody had budget
' approval for anything else.
'
' MIGRATION HISTORY:
'
'   Phase I  - Approved
'   Phase II - Deferred
'   Phase III - Phase II documentation unavailable
'
' Former connection string:
'
'   DATA SOURCE=[REDACTED];PASSWORD=[REMOVED AFTER AUDIT]
'
' The actual value was reportedly written on the underside of
' server BLOAT-DB-02. That server was disposed of in 2017.
'

Imports System.IO
Imports System.Threading
Imports Bloat.Core.Amplification
Imports Microsoft.Data.Sqlite

Namespace Amplification

    Public NotInheritable Class SqliteAmplificationCaseRepository
        Implements IAmplificationCaseRepository

        Private ReadOnly _databasePath As String
        Private ReadOnly _connectionString As String

        Public Sub New(databasePath As String)
            ArgumentException.ThrowIfNullOrWhiteSpace(databasePath)

            _databasePath = Path.GetFullPath(databasePath)

            Dim connectionStringBuilder =
                New SqliteConnectionStringBuilder With {
                    .DataSource = _databasePath,
                    .Mode = SqliteOpenMode.ReadWriteCreate,
                    .Cache = SqliteCacheMode.Shared,
                    .Pooling = False
                }

            _connectionString = connectionStringBuilder.ToString()
        End Sub

        Public Async Function InitializeAsync(Optional cancellationToken As CancellationToken = Nothing) As Task

            Dim databaseDirectory = Path.GetDirectoryName(_databasePath)

            If Not String.IsNullOrWhiteSpace(databaseDirectory) Then
                Directory.CreateDirectory(databaseDirectory)
            End If

            Using connection = CreateConnection()
                Await connection.OpenAsync(cancellationToken)

                Using command = connection.CreateCommand()
                    command.CommandText =
                        "CREATE TABLE IF NOT EXISTS AmplificationCases (" &
                        "Token TEXT NOT NULL PRIMARY KEY, " &
                        "CaseNumber TEXT NOT NULL UNIQUE, " &
                        "OriginalUrl TEXT NOT NULL, " &
                        "AmplifiedRelativeUrl TEXT NOT NULL, " &
                        "CreatedAtUnixMilliseconds INTEGER NOT NULL" &
                        ");"

                    Await command.ExecuteNonQueryAsync(cancellationToken)
                End Using
            End Using

        End Function

        Public Function TryAddAsync(amplificationCase As AmplificationCase, Optional cancellationToken As CancellationToken = Nothing) As ValueTask(Of Boolean) _
            Implements IAmplificationCaseRepository.TryAddAsync

            Return New ValueTask(Of Boolean)(TryAddCoreAsync(amplificationCase, cancellationToken))
        End Function

        Public Function FindByTokenAsync(token As String, Optional cancellationToken As CancellationToken = Nothing) As ValueTask(Of AmplificationCase) _
            Implements IAmplificationCaseRepository.FindByTokenAsync

            Return New ValueTask(Of AmplificationCase)(
                FindByTokenCoreAsync(
                    token,
                    cancellationToken))
        End Function

        Private Async Function TryAddCoreAsync(amplificationCase As AmplificationCase, cancellationToken As CancellationToken) As Task(Of Boolean)

            ArgumentNullException.ThrowIfNull(amplificationCase)
            cancellationToken.ThrowIfCancellationRequested()

            Using connection = CreateConnection()
                Await connection.OpenAsync(cancellationToken)

                Using command = connection.CreateCommand()
                    command.CommandText =
                        "INSERT OR IGNORE INTO AmplificationCases (" &
                        "Token, " &
                        "CaseNumber, " &
                        "OriginalUrl, " &
                        "AmplifiedRelativeUrl, " &
                        "CreatedAtUnixMilliseconds" &
                        ") VALUES (" &
                        "$token, " &
                        "$caseNumber, " &
                        "$originalUrl, " &
                        "$amplifiedRelativeUrl, " &
                        "$createdAtUnixMilliseconds" &
                        ");"

                    command.Parameters.AddWithValue("$token", amplificationCase.Token)
                    command.Parameters.AddWithValue("$caseNumber", amplificationCase.CaseNumber)
                    command.Parameters.AddWithValue("$originalUrl", amplificationCase.OriginalUrl)
                    command.Parameters.AddWithValue("$amplifiedRelativeUrl", amplificationCase.AmplifiedRelativeUrl)
                    command.Parameters.AddWithValue("$createdAtUnixMilliseconds", amplificationCase.CreatedAtUtc.ToUnixTimeMilliseconds())

                    Dim rowsAffected = Await command.ExecuteNonQueryAsync(cancellationToken)

                    Return rowsAffected = 1
                End Using
            End Using

        End Function

        Private Async Function FindByTokenCoreAsync(token As String, cancellationToken As CancellationToken) As Task(Of AmplificationCase)

            ArgumentException.ThrowIfNullOrWhiteSpace(token)
            cancellationToken.ThrowIfCancellationRequested()

            Using connection = CreateConnection()
                Await connection.OpenAsync(cancellationToken)

                Using command = connection.CreateCommand()
                    command.CommandText =
                        "SELECT " &
                        "Token, " &
                        "CaseNumber, " &
                        "OriginalUrl, " &
                        "AmplifiedRelativeUrl, " &
                        "CreatedAtUnixMilliseconds " &
                        "FROM AmplificationCases " &
                        "WHERE Token = $token " &
                        "LIMIT 1;"

                    command.Parameters.AddWithValue("$token", token)

                    Using reader = Await command.ExecuteReaderAsync(cancellationToken)

                        If Not Await reader.ReadAsync(cancellationToken) Then
                            Return Nothing
                        End If

                        Return New AmplificationCase(
                            Token:=reader.GetString(0),
                            CaseNumber:=reader.GetString(1),
                            OriginalUrl:=reader.GetString(2),
                            AmplifiedRelativeUrl:=reader.GetString(3),
                            CreatedAtUtc:=DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64(4)))
                    End Using
                End Using
            End Using

        End Function

        Private Function CreateConnection() As SqliteConnection
            Return New SqliteConnection(_connectionString)
        End Function

    End Class

End Namespace