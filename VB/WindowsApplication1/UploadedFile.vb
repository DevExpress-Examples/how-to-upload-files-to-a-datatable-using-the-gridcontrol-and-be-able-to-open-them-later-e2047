﻿Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics

Namespace WindowsApplication1
	Public Class UploadedFile
		Private tempDirectory As String = "C:\"

		Private _FileName As String
		Public ReadOnly Property FileName() As String
			Get
				Return _FileName
			End Get
		End Property

		Private _FullPath As String
		Public Property FullPath() As String
			Get
				Return _FullPath
			End Get
			Set(ByVal value As String)
				_FullPath = value
			End Set
		End Property

		Public ReadOnly Property FileSize() As Long
			Get
				Return FileContent.Length
			End Get
		End Property

		Private _File() As Byte
		Public ReadOnly Property FileContent() As Byte()
			Get
				Return _File
			End Get
		End Property

		Private Sub ReadFile(ByVal filePath As String)
			Using stream As New FileStream(filePath, FileMode.Open)
				_File = New Byte(stream.Length - 1){}
				stream.Read(FileContent, 0, CInt(stream.Length))
				stream.Close()
			End Using
		End Sub


		Private Shared Sub ReadWriteStream(ByVal buffer() As Byte, ByVal writeStream As Stream)
			writeStream.Write(buffer, 0, buffer.Length)
			writeStream.Close()
		End Sub

'INSTANT VB NOTE: The variable fileContent was renamed since Visual Basic does not handle local variables named the same as class members well:
		Private Function GetContent(ByVal fileContent_Conflict As FileStream) As Byte()
			Dim result(FileSize - 1) As Byte
			fileContent_Conflict.Read(result, 0, CInt(FileSize))
			fileContent_Conflict.Close()
			Return result
		End Function



		Private Shared Sub WriteFile(ByVal filePath As String, ByVal content() As Byte)
			Using writeStream As New FileStream(filePath, FileMode.Create, FileAccess.Write)
				ReadWriteStream(content, writeStream)
				writeStream.Close()
			End Using
		End Sub

		Public Sub OpenFile()
			Process.Start(FullPath)
		End Sub

		Public Sub New(ByVal filePath As String)
			ReadFile(filePath)
			_FileName = Path.GetFileName(filePath)
			_FullPath = filePath
		End Sub


'INSTANT VB NOTE: The variable fileName was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal content() As Byte, ByVal fileName_Conflict As String)
			_FullPath = tempDirectory & fileName_Conflict
			WriteFile(FullPath, content)
			_FileName = fileName_Conflict

		End Sub
	End Class
End Namespace
