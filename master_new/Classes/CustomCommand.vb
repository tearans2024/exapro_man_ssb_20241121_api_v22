Public Class CustomCommand
    Implements IDisposable
    ' Private field to store the command text

    Private _sql As String
    Private _commandText As String

    Private disposed As Boolean = False

    Public Property SQL() As String
        Get
            Return _sql
        End Get
        Set(ByVal value As String)
            _sql = value
        End Set
    End Property

    'Public Property CommandText() As String
    '    Get
    '        Return _commandText
    '    End Get
    '    Set(ByVal value As String)
    '        _commandText = value
    '    End Set
    'End Property

    'Public Function ExecuteNonQuery() As Integer

    '    ' Add the command text to the ArrayList in the parent class
    '    'parent.ssqls.Add(_commandText)
    '    ssqls.Add(_commandText)

    '    ' Simulate the number of affected rows
    '    Dim affectedRows As Integer = 1 ' Example: Assume one row affected

    '    ' Print a message for demonstration purposes
    '    Console.WriteLine("Command executed: " & _commandText)
    '    'Console.WriteLine("Command type: " & _commandType.ToString())
    '    Console.WriteLine("Rows affected: " & affectedRows)

    '    ' Return the number of affected rows
    '    Return affectedRows
    'End Function

    'Public Function Commit() As Boolean
    '    Dim par_error As New ArrayList
    '    If master_new.PGSqlConn.dml(ssqls, par_error) Then
    '        '                Box("Success")

    '        'after_success()
    '        'set_row(_so_oid.ToString, "so_oid")
    '        'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    '        'insert = True
    '        Return True
    '    Else
    '        MessageBox.Show(par_error.Item(0).ToString)

    '        Return False
    '    End If
    'End Function

    ' ArrayList to keep track of all executed SQL command texts
    Public Shared ssqls As New ArrayList()


    Public Sub InitializeCommand()
    End Sub

    
    Public Function ExecuteStoredProcedure() As Boolean
        Dim ssqls1 As New ArrayList
        ssqls1.Add(SQL)
        Dim par_error As New ArrayList
        If master_new.PGSqlConn.dml(ssqls1, par_error) Then
            '                Box("Success")

            'after_success()
            'set_row(_so_oid.ToString, "so_oid")
            'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            'insert = True
            'ssqls.Clear()
            Return True
        Else
            'ssqls.Clear()
            MessageBox.Show(par_error.Item(0).ToString)

            Return False
        End If
        ' Return the number of affected rows
        Return True
    End Function

    Public Sub FillDataSet(ByRef oDataSet As DataSet, ByVal TableName As String)
        Try
            'InitializeCommand()
            'InitializeDataAdapter()
            'DataAdapter.Fill(oDataSet, TableName)

            'oDataSet=master_new.PGSqlConn.GetDataset(CommandText
            master_new.PGSqlConn.GetDataset(SQL, oDataSet, TableName)

        Catch OleDbExceptionErr As Exception
            Throw New System.Exception(OleDbExceptionErr.Message, _
                OleDbExceptionErr.InnerException)
        Finally
            'Command.Dispose()
            'Command = Nothing
            'DataAdapter.Dispose()
            'DataAdapter = Nothing
        End Try
    End Sub

    Public Class CustomCommand
        'Implements IDisposable
        'Private disposed As Boolean = False
        Private _commandText As String

        '' This code added by Visual Basic to correctly implement the disposable pattern.
        'Public Overloads Sub Dispose() Implements IDisposable.Dispose
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose()
        '    GC.SuppressFinalize(Me)
        'End Sub

        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose()
        '    MyBase.Finalize()
        'End Sub
        'add by rana 20241025 - clear query sebelum memulai

        Public Function Open() As Boolean
            ssqls.Clear()
        End Function

        ' Public property for CommandText (like .Command.CommandText)
        Public Property CommandText() As String
            Get
                Return _commandText
            End Get
            Set(ByVal value As String)
                _commandText = value
            End Set
        End Property

        Public Function ExecuteNonQuery() As Integer

            ' Add the command text to the ArrayList in the parent class
            'parent.ssqls.Add(_commandText)
            ssqls.Add(_commandText)

            ' Simulate the number of affected rows
            Dim affectedRows As Integer = 1 ' Example: Assume one row affected

            ' Print a message for demonstration purposes
            Console.WriteLine("Command executed: " & _commandText)
            'Console.WriteLine("Command type: " & _commandType.ToString())
            Console.WriteLine("Rows affected: " & affectedRows)

            ' Return the number of affected rows
            Return affectedRows
        End Function

        Public Function Commit() As Boolean
            Dim par_error As New ArrayList
            If master_new.PGSqlConn.dml(ssqls, par_error) Then
                '                Box("Success")

                'after_success()
                'set_row(_so_oid.ToString, "so_oid")
                'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                'insert = True
                ssqls.Clear()
                Return True
            Else
                ssqls.Clear()
                'MessageBox.Show(par_error.Item(0).ToString)
                Throw New Exception("Terjadi kesalahan dalam proses DML: " & par_error.Item(0).ToString())

                Return False
            End If
        End Function

    End Class

    ' Custom enumeration to mimic the CommandType enum in ADO.NET
    Public Enum CustomCommandType
        Text
        StoredProcedure
        TableDirect
    End Enum
    Public Connection As CustomConnection
    Private _customSqlCommand As CustomSqlCommand
    Public Command As New CustomCommand
    Public DataReader As CustomDataReader


    ' Nested class to simulate a Connection object
    Public Class CustomConnection
        ' Private field to indicate if the connection is open
        Private _isOpen As Boolean = False

        ' Public method to open the connection
        Public Sub Open()
            ' Set the connection status to open
            _isOpen = True
            Console.WriteLine("Connection opened.")
        End Sub

        ' Public property to check if the connection is open
        Public ReadOnly Property IsOpen() As Boolean
            Get
                Return _isOpen
            End Get
        End Property

        ' Public method to create a command object
        Public Function CreateCommand() As CustomSqlCommand
            ' Return a new instance of CustomSqlCommand
            Return New CustomSqlCommand()
        End Function
    End Class
    ' Nested class to simulate a Command object with CommandText
    Public Class CustomSqlCommand
        ' Private field to store the command text
        Private _commandText As String
        Private _commandType As CustomCommandType = CustomCommandType.Text
       
        ' Public property for CommandText
        Public Property CommandText() As String
            Get
                Return _commandText
            End Get
            Set(ByVal value As String)
                _commandText = value
            End Set
        End Property

        ' Public property for CommandType
        Public Property CommandType() As CustomCommandType
            Get
                Return _commandType
            End Get
            Set(ByVal value As CustomCommandType)
                _commandType = value
            End Set
        End Property

       

    End Class



    ' Nested class to simulate a DataReader
    Public Class CustomDataReader

        ' Private fields to simulate data rows and the current position
        Private _data As DataTable 'List(Of Dictionary(Of String, Object))
        Private _currentIndex As Integer

        '' Constructor to initialize the data
        'Public Sub New(ByVal data As List(Of Dictionary(Of String, Object)))
        '    _data = data
        '    _currentIndex = -1 ' Start before the first record
        'End Sub
        Public Sub New(ByVal data As Object)
            _data = data
            _currentIndex = -1 ' Start before the first record
        End Sub

        ' Public method to read the next row (like .DataReader.Read)
        Public Function Read() As Boolean
            ' Increment the current index
            _currentIndex += 1

            ' Check if there are more rows to read
            Return _currentIndex < _data.Rows.Count
        End Function

        ' Public method to get the value of a specific column
        Public Function GetValue(ByVal columnName As String) As Object
            ' Ensure the current index is within bounds
            If _currentIndex >= 0 AndAlso _currentIndex < _data.Rows.Count Then
                Dim currentRow As DataRow = _data.Rows(_currentIndex)
                If currentRow.Table.Columns.Contains(columnName) Then
                    Return currentRow.Item(columnName)
                End If
            End If

            ' Return Nothing if the column or row is out of bounds
            Return Nothing
        End Function

        ' Public property to check if there are any rows
        Public ReadOnly Property HasRows() As Boolean
            Get
                ' Return True if there is at least one row in the data
                Return _data IsNot Nothing AndAlso _data.Rows.Count > 0
            End Get
        End Property

        ' Default property to allow access like .Item("ColumnName")
        Default Public ReadOnly Property Item(ByVal columnName As String) As Object
            Get
                Return GetValue(columnName)
            End Get
        End Property
    End Class
    Dim dt As New DataTable
    ' Method to execute the command and return a CustomDataReader
    Public Function ExecuteReader() As CustomDataReader
        ' Simulate some data as an example
        'Dim sampleData As New List(Of Dictionary(Of String, Object))()

        'Dim row1 As New Dictionary(Of String, Object)()
        'row1("item1") = "Row1Item1"
        'row1("item2") = "Row1Item2"
        'sampleData.Add(row1)

        'Dim row2 As New Dictionary(Of String, Object)()
        'row2("item1") = "Row2Item1"
        'row2("item2") = "Row2Item2"
        'sampleData.Add(row2)


        dt = master_new.PGSqlConn.GetTableData(Command.CommandText)

        ' Return a new instance of CustomDataReader with the sample data
        Return New CustomDataReader(dt)
    End Function

    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' TODO: put code to dispose managed resources
                'If Not DataReader Is Nothing Then
                '    DataReader.Dispose()
                '    DataReader = Nothing
                'End If
                'If Not DataAdapter Is Nothing Then
                '    DataAdapter.Dispose()
                '    DataAdapter = Nothing
                'End If
                'If Not Command Is Nothing Then
                '    Command.Dispose()
                '    Command = Nothing
                'End If
                'If Not Connection Is Nothing Then
                '    Connection.Close()
                '    Connection.Dispose()
                '    Connection = Nothing
                'End If
            End If

            ' TODO: put code to free unmanaged resources here
        End If
        Me.disposed = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region

End Class
