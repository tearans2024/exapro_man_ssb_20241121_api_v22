Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports DevExpress.Data

Namespace DevExpress.XtraGrid.Demos
	#Region "FilesDir"
	Public Class Directories
		Inherits ArrayList
		Implements DevExpress.Data.IRelationList
		Public Sub New(ByVal root As String)
			Dim s As String() = System.IO.Directory.GetDirectories(root)
			Dim i As Integer = 0
			Do While i < s.Length
				Add(New Directory(i, s(i)))
				i += 1
			Loop
		End Sub

		'IRelationList
		Private Function GetRelationName(ByVal index As Integer, ByVal relationIndex As Integer) As String Implements IRelationList.GetRelationName
			If relationIndex = 0 Then
				Return ("Directories")
			Else
				Return ("Files")
			End If
		End Function

		Private ReadOnly Property RelationCount() As Integer Implements IRelationList.RelationCount
			Get
				Return 2
			End Get
		End Property

		Private Function IsMasterRowEmpty(ByVal index As Integer, ByVal relationIndex As Integer) As Boolean Implements IRelationList.IsMasterRowEmpty
			Return Me(index).MasterDetail(relationIndex)
		End Function

		Private Function GetDetailList(ByVal index As Integer, ByVal relationIndex As Integer) As IList Implements IRelationList.GetDetailList
			Dim list As IList
			Dim rootDir As String = (CType(Me(index), Directory)).FullName
			If relationIndex = 0 Then
				list = New Directories(rootDir)
			Else
				list = New Files(rootDir)
			End If
			If list.Count > 0 Then
				Return (list)
			Else
				Return (Nothing)
			End If
		End Function
		'IRelationList end

		Public Overridable Shadows ReadOnly Default Property Item(ByVal index As Integer) As Directory
			Get
				Return CType(MyBase.Item(index), Directory)
			End Get
		End Property
	End Class

	Public Class Files
		Inherits ArrayList
		Public Sub New(ByVal dir As String)
			Dim s As String() = System.IO.Directory.GetFiles(dir)
			Dim i As Integer = 0
			Do While i < s.Length
				Add(New File(s(i)))
				i += 1
			Loop
		End Sub
	End Class

	Public Class Directory
		Private fid As Integer
		Private fname As String
		Private ffilesCount As Integer = -1
		Private fchildDirCount As Integer = -1
		Friend dInfo As System.IO.DirectoryInfo

		Friend Sub New(ByVal fid As Integer, ByVal fname As String)
			Me.fid = fid
			Me.fname = fname
			dInfo = New System.IO.DirectoryInfo(Me.fname)
		End Sub

		Public Function MasterDetail(ByVal relationIndex As Integer) As Boolean
			Dim ret As Boolean = True
			Try
				If relationIndex = 0 Then
					ret = Not(System.IO.Directory.GetDirectories(fname).Length > 0)
				Else
					ret = Not(System.IO.Directory.GetFiles(fname).Length > 0)
				End If
			Catch
			End Try
			Return ret
		End Function

		Private ReadOnly Property ID() As Integer
			Get
				Return fid
			End Get
		End Property

		Public ReadOnly Property FullName() As String
			Get
				Return fname
			End Get
		End Property
		Public ReadOnly Property Name() As String
			Get
				Return dInfo.Name
			End Get
		End Property
		Public ReadOnly Property CreationTime() As DateTime
			Get
				Return dInfo.CreationTime
			End Get
		End Property
		Public ReadOnly Property FilesCount() As Integer
			Get
				If ffilesCount = -1 Then
					ffilesCount = 0
					Try
						ffilesCount = System.IO.Directory.GetFiles(fname).Length
					Catch
					End Try
				End If
				Return ffilesCount
			End Get
		End Property
		Public ReadOnly Property ChildDirCount() As Integer
			Get
				If fchildDirCount = -1 Then
					fchildDirCount = 0
					Try
						fchildDirCount = System.IO.Directory.GetDirectories(fname).Length
					Catch
					End Try
				End If
				Return fchildDirCount
			End Get
		End Property
		Public ReadOnly Property Attributes() As String
			Get
				Return MyFileAttributes.StringFileAttributes(dInfo.Attributes)
			End Get
		End Property
	End Class

	Public Class File
		Friend fInfo As System.IO.FileInfo
		Friend Sub New(ByVal fname As String)
			fInfo = New System.IO.FileInfo(fname)
		End Sub
		Public ReadOnly Property Name() As String
			Get
				Return fInfo.Name
			End Get
		End Property
		Public ReadOnly Property Length() As Long
			Get
				Return fInfo.Length
			End Get
		End Property
		Public ReadOnly Property CreationTime() As DateTime
			Get
				Return fInfo.CreationTime
			End Get
		End Property
		Public ReadOnly Property LastWriteTime() As DateTime
			Get
				Return fInfo.LastWriteTime
			End Get
		End Property
		Public ReadOnly Property LastAccessTime() As DateTime
			Get
				Return fInfo.LastAccessTime
			End Get
		End Property
		Public ReadOnly Property Attributes() As String
			Get
				Return MyFileAttributes.StringFileAttributes(fInfo.Attributes)
			End Get
		End Property
	End Class

	Friend Class MyFileAttributes
		Friend Shared Function StringFileAttributes(ByVal fa As System.IO.FileAttributes) As String
			Dim s As String = ""
			If (System.IO.FileAttributes.Archive And fa) <> 0 Then
				s &= "-A"
			End If
			If (System.IO.FileAttributes.Compressed And fa) <> 0 Then
				s &= "-C"
			End If
			If (System.IO.FileAttributes.Normal And fa) <> 0 Then
				s &= "-N"
			End If
			If (System.IO.FileAttributes.Hidden And fa) <> 0 Then
				s &= "-H"
			End If
			If (System.IO.FileAttributes.ReadOnly And fa) <> 0 Then
				s &= "-RO"
			End If
			If (System.IO.FileAttributes.System And fa) <> 0 Then
				s &= "-S"
			End If
			If (System.IO.FileAttributes.Temporary And fa) <> 0 Then
				s &= "-Tmp"
			End If
			s &= "-"
			Return s
		End Function
	End Class

	#End Region
	#Region "GridFolder"
	Public Enum FileCollectionItemType
		Up = 0
		File
		Directory
	End Enum
	Public Class FileCollectionItem
		Private fitemType As FileCollectionItemType
		Private fname, fdisplayName As String
		Private ffileSize As Long
		Private fmodified As DateTime

		Public Sub New(ByVal fitemType As FileCollectionItemType, ByVal fname As String)
			Me.fitemType = fitemType
			Me.fname = fname
			If ItemType <> FileCollectionItemType.Up Then
				Me.fdisplayName = System.IO.Path.GetFileName(fname)
			Else
				Me.fdisplayName = ".."
			End If
			Me.ffileSize = -1
		End Sub
		Public ReadOnly Property ItemType() As FileCollectionItemType
			Get
				Return Me.fitemType
			End Get
		End Property
		Public ReadOnly Property Name() As String
			Get
				Return Me.fdisplayName
			End Get
		End Property
		Public ReadOnly Property FullName() As String
			Get
				If ItemType = FileCollectionItemType.Up Then
					Return System.IO.Path.GetDirectoryName(Me.fname)
				Else
					Return Me.fname
				End If
			End Get
		End Property
		Public ReadOnly Property DirectoryName() As String
			Get
				If ItemType <> FileCollectionItemType.Up Then
					Return System.IO.Path.GetDirectoryName(Me.fname)
				Else
					Return Me.fname
				End If
			End Get
		End Property
		Public ReadOnly Property ImageIndex() As Integer
			Get
				Return CInt(Fix(ItemType -1))
			End Get
		End Property
		Public ReadOnly Property Size() As Long
			Get
				If ItemType = FileCollectionItemType.File Then
					UpdateFileInfo()
					Return Me.ffileSize
				Else
					Return 0
				End If
			End Get
		End Property
		Public ReadOnly Property Modified() As DateTime
			Get
				If ItemType = FileCollectionItemType.File Then
					UpdateFileInfo()
					Return Me.fmodified
				Else
					UpdateDirInfo()
					Return Me.fmodified
				End If
			End Get
		End Property
		Private Sub UpdateFileInfo()
			If Me.ffileSize >= 0 Then
			Return
			End If
			Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(Me.fname)
			Me.ffileSize = fileInfo.Length
			Me.fmodified = fileInfo.LastWriteTime
		End Sub
		Private Sub UpdateDirInfo()
			Dim dirInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(Me.fname)
			Me.fmodified = dirInfo.LastWriteTime
		End Sub
	End Class
	Friend Class FileCollection
		Inherits ArrayList
		Private ffilePath As String
		Public Sub New(ByVal ffilePath As String)
			FilePath = ffilePath
		End Sub
		Public Property FilePath() As String
			Get
				Return ffilePath
			End Get
			Set
				If FilePath = Value Then
				Return
				End If
				Clear()
				Me.ffilePath = Value
				If System.IO.Path.GetFileName(Value) <> String.Empty Then
					Add(New FileCollectionItem(FileCollectionItemType.Up, Value))
				End If
				Try
					Dim dirs As String() = System.IO.Directory.GetDirectories(ffilePath)
					Dim i As Integer = 0
					Do While i < dirs.Length
						Add(New FileCollectionItem(FileCollectionItemType.Directory, dirs(i)))
						i += 1
					Loop
					Dim files As String() = System.IO.Directory.GetFiles(ffilePath)
					i = 0
					Do While i < files.Length
						Add(New FileCollectionItem(FileCollectionItemType.File, files(i)))
						i += 1
					Loop
				Catch
				End Try
			End Set
		End Property
		Public Shadows ReadOnly Default Property Item(ByVal index As Integer) As FileCollectionItem
			Get
				Return TryCast(MyBase.Item(index), FileCollectionItem)
			End Get
		End Property
		Public Overloads Function IndexOf(ByVal path As String) As Integer
			For i As Integer = 0 To Count - 1
				If path.Equals(Me(i).FullName) Then
				Return i
				End If
			Next i
			Return -1
		End Function
	End Class
	#End Region
End Namespace
