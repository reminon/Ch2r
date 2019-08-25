Imports System.IO
Imports System.Drawing
Imports System.Runtime.InteropServices

'//////////////////////////////////////////////
'//
'// Originaly from Halo Map Tools
'// Edited by XIU <xiu@shoeke.com>
'//
'//////////////////////////////////////////////

Public Class H2Bitmap
  Inherits System.Windows.Forms.UserControl

  Private _stream As FileStream
  Private _shared As FileStream ' Used for MP and SP
  Private _mainmenu As FileStream ' Used for MP and SP
  Private _spshared As FileStream ' Used for SP
  Private _filename As String
  Public b As Bitmap
  Public ptr As New System.IntPtr
  <MarshalAs(UnmanagedType.ByValArray)> Public decodedChunk() As Byte

#Region " Windows Form Designer generated code "

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call

  End Sub

  'UserControl overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents sizeTextBox As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents partsTextBox As System.Windows.Forms.TextBox
  Friend WithEvents bitmapSaveFileDialog As System.Windows.Forms.SaveFileDialog
  Friend WithEvents extractButton As System.Windows.Forms.Button
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents formatTextBox As System.Windows.Forms.TextBox
  Friend WithEvents imagesGroupBox As System.Windows.Forms.GroupBox
  Friend WithEvents imagesListBox As System.Windows.Forms.ListBox
  Friend WithEvents previewPictureBox As System.Windows.Forms.PictureBox
  Friend WithEvents panel As System.Windows.Forms.Panel
  Friend WithEvents previewPanel As System.Windows.Forms.Panel
  Friend WithEvents previewLabel As System.Windows.Forms.Label
  Friend WithEvents injectButton As System.Windows.Forms.Button
  Friend WithEvents bitmapOpenFileDialog As System.Windows.Forms.OpenFileDialog
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.Label4 = New System.Windows.Forms.Label
    Me.sizeTextBox = New System.Windows.Forms.TextBox
    Me.Label5 = New System.Windows.Forms.Label
    Me.partsTextBox = New System.Windows.Forms.TextBox
    Me.bitmapSaveFileDialog = New System.Windows.Forms.SaveFileDialog
    Me.extractButton = New System.Windows.Forms.Button
    Me.Label6 = New System.Windows.Forms.Label
    Me.formatTextBox = New System.Windows.Forms.TextBox
    Me.imagesGroupBox = New System.Windows.Forms.GroupBox
    Me.imagesListBox = New System.Windows.Forms.ListBox
    Me.previewPanel = New System.Windows.Forms.Panel
    Me.previewLabel = New System.Windows.Forms.Label
    Me.previewPictureBox = New System.Windows.Forms.PictureBox
    Me.panel = New System.Windows.Forms.Panel
    Me.injectButton = New System.Windows.Forms.Button
    Me.bitmapOpenFileDialog = New System.Windows.Forms.OpenFileDialog
    Me.imagesGroupBox.SuspendLayout()
    Me.previewPanel.SuspendLayout()
    Me.panel.SuspendLayout()
    Me.SuspendLayout()
    '
    'Label4
    '
    Me.Label4.Location = New System.Drawing.Point(8, 32)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(64, 16)
    Me.Label4.TabIndex = 6
    Me.Label4.Text = "Total Size:"
    '
    'sizeTextBox
    '
    Me.sizeTextBox.Location = New System.Drawing.Point(80, 32)
    Me.sizeTextBox.Name = "sizeTextBox"
    Me.sizeTextBox.ReadOnly = True
    Me.sizeTextBox.Size = New System.Drawing.Size(80, 20)
    Me.sizeTextBox.TabIndex = 7
    Me.sizeTextBox.Text = ""
    '
    'Label5
    '
    Me.Label5.Location = New System.Drawing.Point(8, 8)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(48, 16)
    Me.Label5.TabIndex = 8
    Me.Label5.Text = "Parts:"
    '
    'partsTextBox
    '
    Me.partsTextBox.Location = New System.Drawing.Point(80, 8)
    Me.partsTextBox.Name = "partsTextBox"
    Me.partsTextBox.ReadOnly = True
    Me.partsTextBox.Size = New System.Drawing.Size(80, 20)
    Me.partsTextBox.TabIndex = 9
    Me.partsTextBox.Text = ""
    '
    'bitmapSaveFileDialog
    '
    Me.bitmapSaveFileDialog.DefaultExt = "dds"
    Me.bitmapSaveFileDialog.Filter = "Bitmap File (*.dds)|*.dds"
    Me.bitmapSaveFileDialog.Title = "Save bitmap to..."
    '
    'extractButton
    '
    Me.extractButton.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.extractButton.Location = New System.Drawing.Point(176, 16)
    Me.extractButton.Name = "extractButton"
    Me.extractButton.Size = New System.Drawing.Size(88, 24)
    Me.extractButton.TabIndex = 10
    Me.extractButton.Text = "&Extract"
    '
    'Label6
    '
    Me.Label6.Location = New System.Drawing.Point(8, 56)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(72, 16)
    Me.Label6.TabIndex = 11
    Me.Label6.Text = "Format:"
    '
    'formatTextBox
    '
    Me.formatTextBox.Location = New System.Drawing.Point(80, 56)
    Me.formatTextBox.Name = "formatTextBox"
    Me.formatTextBox.ReadOnly = True
    Me.formatTextBox.Size = New System.Drawing.Size(80, 20)
    Me.formatTextBox.TabIndex = 12
    Me.formatTextBox.Text = ""
    '
    'imagesGroupBox
    '
    Me.imagesGroupBox.Controls.Add(Me.imagesListBox)
    Me.imagesGroupBox.Controls.Add(Me.previewPanel)
    Me.imagesGroupBox.Dock = System.Windows.Forms.DockStyle.Top
    Me.imagesGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.imagesGroupBox.Location = New System.Drawing.Point(0, 0)
    Me.imagesGroupBox.Name = "imagesGroupBox"
    Me.imagesGroupBox.Size = New System.Drawing.Size(480, 291)
    Me.imagesGroupBox.TabIndex = 13
    Me.imagesGroupBox.TabStop = False
    Me.imagesGroupBox.Text = "Images:"
    '
    'imagesListBox
    '
    Me.imagesListBox.Dock = System.Windows.Forms.DockStyle.Fill
    Me.imagesListBox.Location = New System.Drawing.Point(3, 16)
    Me.imagesListBox.Name = "imagesListBox"
    Me.imagesListBox.Size = New System.Drawing.Size(218, 264)
    Me.imagesListBox.TabIndex = 0
    '
    'previewPanel
    '
    Me.previewPanel.Controls.Add(Me.previewLabel)
    Me.previewPanel.Controls.Add(Me.previewPictureBox)
    Me.previewPanel.Dock = System.Windows.Forms.DockStyle.Right
    Me.previewPanel.Location = New System.Drawing.Point(221, 16)
    Me.previewPanel.Name = "previewPanel"
    Me.previewPanel.Size = New System.Drawing.Size(256, 272)
    Me.previewPanel.TabIndex = 15
    '
    'previewLabel
    '
    Me.previewLabel.Dock = System.Windows.Forms.DockStyle.Fill
    Me.previewLabel.Location = New System.Drawing.Point(0, 256)
    Me.previewLabel.Name = "previewLabel"
    Me.previewLabel.Size = New System.Drawing.Size(256, 16)
    Me.previewLabel.TabIndex = 15
    Me.previewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'previewPictureBox
    '
    Me.previewPictureBox.BackColor = System.Drawing.Color.Black
    Me.previewPictureBox.Dock = System.Windows.Forms.DockStyle.Top
    Me.previewPictureBox.Location = New System.Drawing.Point(0, 0)
    Me.previewPictureBox.Name = "previewPictureBox"
    Me.previewPictureBox.Size = New System.Drawing.Size(256, 256)
    Me.previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.previewPictureBox.TabIndex = 14
    Me.previewPictureBox.TabStop = False
    '
    'panel
    '
    Me.panel.Controls.Add(Me.injectButton)
    Me.panel.Controls.Add(Me.formatTextBox)
    Me.panel.Controls.Add(Me.sizeTextBox)
    Me.panel.Controls.Add(Me.Label6)
    Me.panel.Controls.Add(Me.Label4)
    Me.panel.Controls.Add(Me.extractButton)
    Me.panel.Controls.Add(Me.Label5)
    Me.panel.Controls.Add(Me.partsTextBox)
    Me.panel.Dock = System.Windows.Forms.DockStyle.Fill
    Me.panel.Location = New System.Drawing.Point(0, 291)
    Me.panel.Name = "panel"
    Me.panel.Size = New System.Drawing.Size(480, 117)
    Me.panel.TabIndex = 14
    '
    'injectButton
    '
    Me.injectButton.Enabled = False
    Me.injectButton.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.injectButton.Location = New System.Drawing.Point(176, 48)
    Me.injectButton.Name = "injectButton"
    Me.injectButton.Size = New System.Drawing.Size(88, 24)
    Me.injectButton.TabIndex = 13
    Me.injectButton.Text = "&Inject"
    '
    'bitmapOpenFileDialog
    '
    Me.bitmapOpenFileDialog.DefaultExt = "dds"
    Me.bitmapOpenFileDialog.Filter = "DDS Files (*.dds)|*.dds"
    '
    'H2Bitmap
    '
    Me.Controls.Add(Me.panel)
    Me.Controls.Add(Me.imagesGroupBox)
    Me.Name = "H2Bitmap"
    Me.Size = New System.Drawing.Size(480, 408)
    Me.imagesGroupBox.ResumeLayout(False)
    Me.previewPanel.ResumeLayout(False)
    Me.panel.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

#Region "Constants"

#Region "Bitm"

  'BITM
  Enum bitmEnum As Integer
    BITM_FORMAT_A8 = &H0
    BITM_FORMAT_Y8 = &H1
    BITM_FORMAT_AY8 = &H2
    BITM_FORMAT_A8Y8 = &H3
    BITM_FORMAT_R5G6B5 = &H6
    BITM_FORMAT_A1R5G5B5 = &H8
    BITM_FORMAT_A4R4G4B4 = &H9
    BITM_FORMAT_X8R8G8B8 = &HA
    BITM_FORMAT_A8R8G8B8 = &HB
    BITM_FORMAT_DXT1 = &HE
    BITM_FORMAT_DXT2AND3 = &HF
    BITM_FORMAT_DXT4AND5 = &H10
    BITM_FORMAT_P8 = &H11

    BITM_TYPE_2D = &H0
    BITM_TYPE_3D = &H1
    BITM_TYPE_CUBEMAP = &H2

    BITM_FLAG_LINEAR = (1 << 4)
  End Enum

#End Region

#Region "DDS"

  'DDS
  'The dwFlags member of the modified DDSURFACEDESC2 structure can be set to one or more of the following values.
  Enum DDSEnum As Integer
    DDSD_CAPS = &H1
    DDSD_HEIGHT = &H2
    DDSD_WIDTH = &H4
    DDSD_PITCH = &H8
    DDSD_PIXELFORMAT = &H1000
    DDSD_MIPMAPCOUNT = &H20000
    DDSD_LINEARSIZE = &H80000
    DDSD_DEPTH = &H800000

    DDPF_ALPHAPIXELS = &H1
    DDPF_FOURCC = &H4
    DDPF_RGB = &H40

    'The dwCaps1 member of the DDSCAPS2 structure can be set to one or more of the following values.
    DDSCAPS_COMPLEX = &H8
    DDSCAPS_TEXTURE = &H1000
    DDSCAPS_MIPMAP = &H400000

    'The dwCaps2 member of the DDSCAPS2 structure can be set to one or more of the following values.
    DDSCAPS2_CUBEMAP = &H200
    DDSCAPS2_CUBEMAP_POSITIVEX = &H400
    DDSCAPS2_CUBEMAP_NEGATIVEX = &H800
    DDSCAPS2_CUBEMAP_POSITIVEY = &H1000
    DDSCAPS2_CUBEMAP_NEGATIVEY = &H2000
    DDSCAPS2_CUBEMAP_POSITIVEZ = &H4000
    DDSCAPS2_CUBEMAP_NEGATIVEZ = &H8000
    DDSCAPS2_VOLUME = &H200000
  End Enum

#End Region

#End Region

#Region "Structures"

#Region "Bitmap Meta Item"
  Public Structure BITM_HEADER_STRUCT
    Private _width As Short
    Private _height As Short
    Private _format As Short
    Private _offset() As Integer
    Private _size() As Integer
    Private _parts As Integer
    Private _totalsize As Integer
    Private _itemoffset As Long

    Public Function Unsigned(ByVal i As Integer) As Long
      Unsigned = Val("&H" & Hex(i))
      If Unsigned < 0 Then
        Unsigned = Unsigned + (2 ^ 32)
      End If
    End Function

    ' Check this before the others
    Public ReadOnly Property NeedsSpShared() As Boolean
      Get
        Return (Unsigned(_offset(0)) + &H80000000) > &H40000000
      End Get
    End Property

    Public ReadOnly Property NeedsShared() As Boolean
      Get
        Return _offset(0) < 0
      End Get
    End Property

    Public ReadOnly Property NeedsMainmenu() As Boolean
      Get
        Return _offset(0) > &H40000000
      End Get
    End Property

    Public Sub Read(ByRef br As BinaryReader)
      ' Save current position
      _itemoffset = br.BaseStream.Position

      ReDim _offset(6)
      ReDim _size(6)
      Dim i As Integer

      _parts = 0
      _totalsize = 0

      br.ReadInt32() ' Skip 'bitm'
      _width = br.ReadInt16
      _height = br.ReadInt16
      br.ReadInt32()
      _format = br.ReadInt16
      br.BaseStream.Position += &HE

      ' Read raw offsets
      For i = 0 To 5
        _offset(i) = br.ReadInt32
        If _offset(i) = -1 And _parts = 0 Then
          _parts = i
        End If
      Next
      For i = 0 To 5
        _size(i) = br.ReadInt32
      Next

      For i = 0 To _parts - 1
        _totalsize += _size(i)
      Next
    End Sub

    Public Sub Write(ByRef bw As BinaryWriter)
      bw.BaseStream.Position = _itemoffset
      bw.Write(("bitm").ToCharArray)
      bw.Write(_width)
      bw.Write(_height)
      bw.BaseStream.Position += 4
      bw.Write(_format)
      bw.BaseStream.Position += &HE

    End Sub

    Public Function ReadBin(ByVal stream As FileStream) As Byte()
      Dim result(_totalsize) As Byte
      Dim tmp() As Byte
      Dim pos As Integer = 0
      Dim br As BinaryReader = New BinaryReader(stream)

      For i As Integer = 0 To _parts - 1
        If NeedsSpShared Then
          br.BaseStream.Position = Unsigned(_offset(i)) + &H80000000 - &H40000000
        ElseIf NeedsShared Then
          br.BaseStream.Position = Unsigned(_offset(i)) + &H80000000
        ElseIf NeedsMainmenu Then
          br.BaseStream.Position = Unsigned(_offset(i)) - &H40000000
        Else
          br.BaseStream.Position = _offset(i)
        End If
        tmp = br.ReadBytes(_size(i))
        tmp.CopyTo(result, pos)
        pos += _size(i)
      Next

      Return result
    End Function

    Public Sub WriteBin(ByRef bw As BinaryWriter, ByVal data() As Byte)
      Dim pos As Long = 0
      Dim chunk() As Byte

      For i As Integer = 0 To _parts - 1
        If NeedsSpShared Then
          bw.BaseStream.Position = Unsigned(_offset(i)) + &H80000000 - &H40000000
        ElseIf NeedsShared Then
          bw.BaseStream.Position = Unsigned(_offset(i)) + &H80000000
        ElseIf NeedsMainmenu Then
          bw.BaseStream.Position = Unsigned(_offset(i)) - &H40000000
        Else
          bw.BaseStream.Position = _offset(i)
        End If
        ReDim chunk(_size(i))
        Array.Copy(data, pos, chunk, 0, _size(i))
        bw.Write(chunk)
        pos += _size(i)
      Next
    End Sub

    Public Property Width() As Short
      Get
        Return _width
      End Get
      Set(ByVal Value As Short)
        _width = Value
      End Set
    End Property

    Public Property Height() As Short
      Get
        Return _height
      End Get
      Set(ByVal Value As Short)
        _height = Value
      End Set
    End Property

    Public ReadOnly Property Parts() As Integer
      Get
        Return _parts
      End Get
    End Property

    Public ReadOnly Property TotalSize() As Integer
      Get
        Return _totalsize
      End Get
    End Property

    Public ReadOnly Property Format() As Short
      Get
        Return _format
      End Get
    End Property
  End Structure

#End Region

#Region "DDS Structures"

  Public Structure DDS_HEADER_STRUCTURE
    Public magic As String '"DDS "
    Public ddsd As DDSURFACEDESC2
    Public Sub readStruct(ByRef br As BinaryReader)
      magic = br.ReadChars(4)
      ddsd.readStruct(br)
    End Sub
    Public Sub writeStruct(ByRef bw As BinaryWriter)
      bw.BaseStream.WriteByte(Asc(Mid(magic, 1, 1)))
      bw.BaseStream.WriteByte(Asc(Mid(magic, 2, 1)))
      bw.BaseStream.WriteByte(Asc(Mid(magic, 3, 1)))
      bw.BaseStream.WriteByte(Asc(Mid(magic, 4, 1)))
      ddsd.writeStruct(bw)
    End Sub
    '//////////////////////////////////////////////////////
    '// Generate a DDS Header
    '//////////////////////////////////////////////////////
    Public Sub generate(ByRef b2 As BITM_HEADER_STRUCT)
      magic = "DDS "
      ddsd.generate(b2)
    End Sub
  End Structure

  Public Structure DDSURFACEDESC2
    Public size_of_structure As Integer  '124
    Public flags As Integer
    Public height As Integer
    Public width As Integer
    Public PitchOrLinearSize As Integer
    Public depth As Integer 'Only used for volume textures.
    Public MipMapCount As Integer 'Total Number of MipMaps
    Public Reserved1() As Integer '11 ints long
    Public ddfPixelFormat As DDPIXELFORMAT
    Public ddsCaps As DDSCAPS2
    Public Reserved2 As Integer
    Public Sub readStruct(ByRef br As BinaryReader)
      size_of_structure = br.ReadInt32
      flags = br.ReadInt32
      height = br.ReadInt32
      width = br.ReadInt32
      PitchOrLinearSize = br.ReadInt32
      depth = br.ReadInt32
      MipMapCount = br.ReadInt32
      Dim x As Integer 'Loop counter
      ReDim Reserved1(11)
      For x = 1 To 11
        Reserved1(x) = br.ReadInt32
      Next
      ddfPixelFormat.readStruct(br)
      ddsCaps.readStruct(br)
      Reserved2 = br.ReadInt32
    End Sub
    Public Sub writeStruct(ByRef bw As BinaryWriter)
      bw.Write(size_of_structure)
      bw.Write(flags)
      bw.Write(height)
      bw.Write(width)
      bw.Write(PitchOrLinearSize)
      bw.Write(depth)
      bw.Write(MipMapCount)
      Dim x As Integer 'Loop counter
      For x = 1 To 11
        bw.Write(Reserved1(x))
      Next
      ddfPixelFormat.writeStruct(bw)
      ddsCaps.writeStruct(bw)
      bw.Write(Reserved2)
    End Sub
    '//////////////////////////////////////////////////////
    '// Generate the DDSURFACEDESC2 chunk of the header
    '//////////////////////////////////////////////////////
    Public Sub generate(ByVal b2 As BITM_HEADER_STRUCT)
      'Size of structure. This member must be set to 124.
      size_of_structure = 124

      'Flags to indicate valid fields. Always include DDSD_CAPS, DDSD_PIXELFORMAT,
      'DDSD_WIDTH, DDSD_HEIGHT and either DDSD_PITCH or DDSD_LINEARSIZE.
      flags = flags + DDSEnum.DDSD_CAPS
      flags = flags + DDSEnum.DDSD_PIXELFORMAT
      flags = flags + DDSEnum.DDSD_WIDTH
      flags = flags + DDSEnum.DDSD_HEIGHT
      Select Case b2.Format
        Case bitmEnum.BITM_FORMAT_DXT1, bitmEnum.BITM_FORMAT_DXT2AND3, bitmEnum.BITM_FORMAT_DXT4AND5
          flags = flags + DDSEnum.DDSD_LINEARSIZE
        Case Else
          flags = flags + DDSEnum.DDSD_PITCH
      End Select

      'Height of the main image in pixels
      height = b2.Height

      'Width of the main image in pixels
      width = b2.Width

      'For uncompressed formats, this is the number of bytes per scan line (DWORD aligned) for the main
      'image. dwFlags should include DDSD_PITCH in this case. For compressed formats, this is the (total)
      'number of bytes for the main image. dwFlags should be include DDSD_LINEARSIZE in this case.
      Dim RGBBitCount As Integer = 0
      Select Case b2.Format
        Case bitmEnum.BITM_FORMAT_R5G6B5
          RGBBitCount = 16
        Case bitmEnum.BITM_FORMAT_A1R5G5B5
          RGBBitCount = 16
        Case bitmEnum.BITM_FORMAT_A4R4G4B4
          RGBBitCount = 16
        Case bitmEnum.BITM_FORMAT_X8R8G8B8
          RGBBitCount = 32
        Case bitmEnum.BITM_FORMAT_A8R8G8B8
          RGBBitCount = 32
      End Select
      'If (b2.flags And DDSEnum.DDSD_PITCH) Then _
      'PitchOrLinearSize = b2.Width * (RGBBitCount / 8)
      'If (b2.flags And DDSEnum.DDSD_LINEARSIZE) Then _
      'PitchOrLinearSize = b2.size 'I don't think this is correct.
      PitchOrLinearSize = b2.Width * (RGBBitCount / 8)
      'For volume textures, this is the depth of the volume.
      'dwFlags should include DDSD_DEPTH in this case.
      depth = 0 'There are no volume textures (that I know of) in Halo.

      'For items with mipmap levels, this is the total number of levels in the mipmap
      'chain of the main image. dwFlags should include DDSD_MIPMAPCOUNT in this case
      MipMapCount = 0 'b2.num_mipmaps
      If MipMapCount > 0 Then flags = flags + DDSEnum.DDSD_MIPMAPCOUNT


      'Reserved values - should always equal 0.
      ReDim Reserved1(11)
      Reserved1(1) = 0 : Reserved1(2) = 0 : Reserved1(3) = 0 : Reserved1(4) = 0
      Reserved1(5) = 0 : Reserved1(6) = 0 : Reserved1(7) = 0 : Reserved1(8) = 0
      Reserved1(9) = 0 : Reserved1(10) = 0 : Reserved1(11) = 0

      'A 32-byte value that specifies the pixel format structure.
      ddfPixelFormat.generate(b2)

      'A 16-byte value that specifies the capabilities structure.
      ddsCaps.generate(b2)
    End Sub
  End Structure

  Public Structure DDPIXELFORMAT
    Public size As Integer   '32
    Public Flags As Integer  '4
    Public FourCC As String 'DXT1, DXT2, etc..
    Public RGBBitCount As Integer
    Public RBitMask As Integer
    Public GBitMask As Integer
    Public BBitMask As Integer
    Public RGBAlphaBitMask As Integer
    Public Sub readStruct(ByRef br As BinaryReader)
      size = br.ReadInt32 '32
      Flags = br.ReadInt32 '4
      FourCC = br.ReadChars(4) 'DXT1, DXT2, etc..
      RGBBitCount = br.ReadInt32
      RBitMask = br.ReadInt32
      GBitMask = br.ReadInt32
      BBitMask = br.ReadInt32
      RGBAlphaBitMask = br.ReadInt32
    End Sub
    Public Sub writeStruct(ByRef bw As BinaryWriter)
      bw.Write(size)
      bw.Write(Flags)
      bw.BaseStream.WriteByte(Asc(Mid(FourCC, 1, 1)))
      bw.BaseStream.WriteByte(Asc(Mid(FourCC, 2, 1)))
      bw.BaseStream.WriteByte(Asc(Mid(FourCC, 3, 1)))
      bw.BaseStream.WriteByte(Asc(Mid(FourCC, 4, 1)))
      bw.Write(RGBBitCount)
      bw.Write(RBitMask)
      bw.Write(GBitMask)
      bw.Write(BBitMask)
      bw.Write(RGBAlphaBitMask)
    End Sub
    '//////////////////////////////////////////////////////
    '// Generate the DDPIXELFORMAT chunk of the header
    '//////////////////////////////////////////////////////
    Public Sub generate(ByVal b2 As BITM_HEADER_STRUCT)
      'Size of structure. This member must be set to 32. 
      size = 32

      'Flags to indicate valid fields. Uncompressed formats will usually use DDPF_RGB to indicate
      'an RGB format, while compressed formats will use DDPF_FOURCC with a four-character code. 
      'This is accomplished in the below structure.

      'This is the four-character code for compressed formats. dwFlags should include DDPF_FOURCC in
      'this case. For DXTn compression, this is set to "DXT1", "DXT2", "DXT3", "DXT4", or "DXT5". 
      Flags = 0
      Select Case b2.Format
        Case &HE
          FourCC = "DXT1"
          Flags = Flags + DDSEnum.DDPF_FOURCC
        Case &HF
          FourCC = "DXT3"
          Flags = Flags + DDSEnum.DDPF_FOURCC
        Case &H10
          FourCC = "DXT4"
          Flags = Flags + DDSEnum.DDPF_FOURCC
        Case Else
          FourCC = Chr(0) & Chr(0) & Chr(0) & Chr(0)
          Flags = Flags + DDSEnum.DDPF_RGB
      End Select

      'For RGB formats, this is the total number of bits in the format. dwFlags should include DDPF_RGB
      'in this case. This value is usually 16, 24, or 32. For A8R8G8B8, this value would be 32. 
      Select Case b2.Format
        Case bitmEnum.BITM_FORMAT_R5G6B5
          RGBBitCount = 16
        Case bitmEnum.BITM_FORMAT_A1R5G5B5
          RGBBitCount = 16
          Flags = Flags + DDSEnum.DDPF_ALPHAPIXELS
        Case bitmEnum.BITM_FORMAT_A4R4G4B4
          RGBBitCount = 16
          Flags = Flags + DDSEnum.DDPF_ALPHAPIXELS
        Case bitmEnum.BITM_FORMAT_X8R8G8B8
          Flags = Flags + DDSEnum.DDPF_ALPHAPIXELS
          RGBBitCount = 32
        Case bitmEnum.BITM_FORMAT_A8R8G8B8
          Flags = Flags + DDSEnum.DDPF_ALPHAPIXELS
          RGBBitCount = 32
        Case Else
          RGBBitCount = 0
      End Select

      'For RGB formats, this contains the masks for the red, green, and blue channels. For A8R8G8B8, these
      'values would be 0x00ff0000, 0x0000ff00, and 0x000000ff respectively. 
      Select Case b2.Format
        Case bitmEnum.BITM_FORMAT_R5G6B5
          RBitMask = &HF800
          GBitMask = &H7E0
          BBitMask = &H1F
          RGBAlphaBitMask = &H0
        Case bitmEnum.BITM_FORMAT_A1R5G5B5
          RBitMask = &H7C00
          GBitMask = &H3E0
          BBitMask = &H1F
          RGBAlphaBitMask = &H8000
        Case bitmEnum.BITM_FORMAT_A4R4G4B4
          RBitMask = &HF000
          GBitMask = &HF0
          BBitMask = &HF
          RGBAlphaBitMask = &HF00000
        Case bitmEnum.BITM_FORMAT_X8R8G8B8
          RBitMask = &HFF000
          GBitMask = &HFF00
          BBitMask = &HFF
          RGBAlphaBitMask = &HFF000000
        Case bitmEnum.BITM_FORMAT_A8R8G8B8
          RBitMask = &HFF0000
          GBitMask = &HFF00
          BBitMask = &HFF
          RGBAlphaBitMask = &HFF000000
        Case Else
          RBitMask = 0
          GBitMask = 0
          BBitMask = 0
          RGBAlphaBitMask = &H0
      End Select

      'For RGB formats, this contains the mask for the alpha channel, if any. dwFlags should include 
      'DDPF_ALPHAPIXELS in this case. For A8R8G8B8, this value would be 0xff000000. 
      '^^ This was set in the previous block of code.
    End Sub
  End Structure

  Public Structure DDSCAPS2
    Public caps1 As Integer
    Public caps2 As Integer
    Public Reserved() As Integer 'Length of 3 (or maybe 2)
    Public Sub readStruct(ByRef br As BinaryReader)
      caps1 = br.ReadInt32
      caps2 = br.ReadInt32
      ReDim Reserved(2)
      Reserved(1) = br.ReadInt32
      Reserved(2) = br.ReadInt32
    End Sub
    Public Sub writeStruct(ByRef bw As BinaryWriter)
      bw.Write(caps1)
      bw.Write(caps2)
      bw.Write(Reserved(1))
      bw.Write(Reserved(2))
    End Sub
    '//////////////////////////////////////////////////////
    '// Generate the DDSCAPS2 chunk of the header
    '//////////////////////////////////////////////////////
    Public Sub generate(ByRef b2 As BITM_HEADER_STRUCT)
      'DDS files should always include DDSCAPS_TEXTURE. If the file contains mipmaps, DDSCAPS_MIPMAP
      'should be set. For any DDS file with more than one main surface,such as a mipmaps, cubic
      'environment map, or volume texture, DDSCAPS_COMPLEX should also be set. 
      caps1 = caps1 + DDSEnum.DDSCAPS_TEXTURE
      'If b2.num_mipmaps > 0 Then caps1 = caps1 + DDSEnum.DDSCAPS_MIPMAP
      caps1 = caps1 + DDSEnum.DDSCAPS_COMPLEX

      'For cubic environment maps, DDSCAPS2_CUBEMAP should be included as well as one or more faces of
      'the map (DDSCAPS2_CUBEMAP_POSITIVEX, DDSCAPS2_CUBEMAP_NEGATIVEX, DDSCAPS2_CUBEMAP_POSITIVEY,
      ' DDSCAPS2_CUBEMAP_NEGATIVEY, DDSCAPS2_CUBEMAP_POSITIVEZ, DDSCAPS2_CUBEMAP_NEGATIVEZ). For volume
      'textures, DDSCAPS2_VOLUME should be included. 
      '******************************************************************************
      'This is where I stopped - this code needs to be added for cubemaps.
      '******************************************************************************
      caps2 = 0

      'Reserved - all should be set to 0.
      ReDim Reserved(2) : Reserved(1) = 0 : Reserved(2) = 0
    End Sub
  End Structure

#End Region

#End Region

#Region "Header"
  Private bitmheader As BitmapHeader


  Public Class BitmapHeader
    Public _items() As BITM_HEADER_STRUCT
    Private _count As Integer
    Private _offset As Long

    Public Function Unsigned(ByVal i As Integer) As Long
      Unsigned = Val("&H" & Hex(i))
      If Unsigned < 0 Then
        Unsigned = Unsigned + (2 ^ 32)
      End If
    End Function

    Public Sub Read(ByRef br As BinaryReader, ByVal magic As Long)
      br.BaseStream.Position += &H44

      _count = br.ReadInt32
      _offset = Unsigned(br.ReadInt32) - magic

      ReDim _items(_count)
      br.BaseStream.Position = _offset

      For i As Integer = 0 To _count - 1
        br.BaseStream.Position = _offset + i * &H74
        _items(i) = New BITM_HEADER_STRUCT
        _items(i).Read(br)
      Next
    End Sub

    Public ReadOnly Property Count() As Integer
      Get
        Return _count
      End Get
    End Property
  End Class
#End Region

#Region "Swizzle"
  Public Class Swizzle

    Public m_MaskX As Integer = 0
    Public m_MaskY As Integer = 0
    Public m_MaskZ As Integer = 0
    Public Sub New(ByVal width As Integer, ByVal height As Integer, ByVal depth As Integer)
      Dim bit As Integer = 1
      Dim idx As Integer = 1

      While (bit < width) Or (bit < height) Or (bit < depth)

        If (bit < width) Then
          m_MaskX = m_MaskX Or idx
          idx <<= 1
        End If

        If (bit < height) Then
          m_MaskY = m_MaskY Or idx
          idx <<= 1
        End If

        If (bit < depth) Then
          m_MaskZ = m_MaskZ Or idx
          idx <<= 1
        End If

        bit <<= 1
      End While
    End Sub

    Public Function Swizzle(ByVal Sx As Integer, ByVal Sy As Integer, Optional ByVal Sz As Integer = -1) As Int64
      Swizzle = SwizzleAxis(Sx, m_MaskX) Or SwizzleAxis(Sy, m_MaskY) Or (IIf(Sz <> -1, SwizzleAxis(Sz, m_MaskZ), 0))
    End Function

    Public Function SwizzleAxis(ByVal Value As Integer, ByVal Mask As Integer) As Int64

      Dim Result As Int64
      Dim bit As Integer = 1

      While bit <= Mask
        If (Mask And bit) Then
          Result = Result Or (Value And bit)
        Else
          Value <<= 1
        End If
        bit <<= 1
      End While
      SwizzleAxis = Result
    End Function

  End Class

  Public Function ImageType(ByVal i As Integer) As String
    Select Case i
      Case &H0
        ImageType = "A8"
      Case &H1
        ImageType = "Y8"
      Case &H2
        ImageType = "AY8"
      Case &H3
        ImageType = "A8Y8"
      Case &H6
        ImageType = "R5G6B5"
      Case &H8
        ImageType = "A1R5G5B5"
      Case &H9
        ImageType = "A4R4G4B4"
      Case &HA
        ImageType = "X8R8G8B8"
      Case &HB
        ImageType = "A8R8G8B8"
      Case &HE
        ImageType = "DXT1"
      Case &HF
        ImageType = "DXT2/DXT3"
      Case &H10
        ImageType = "DXT4/DXT5"
      Case &H11
        ImageType = "P8"
      Case Else
        ImageType = "Unknown"
    End Select
  End Function

  '/////////////////////////////////////////
  '// Swizzle a set of Pixels
  '// Donated by Stephen Cakebread
  '/////////////////////////////////////////
  Public Enum SwizzleType As Byte
    Swizzle = &H0
    DeSwizzle = &H1
  End Enum

  Public Function SwizzlePicture(ByVal bin() As Byte, ByVal width As Integer, ByVal height As Integer, _
  ByVal depth As Integer, ByVal BitCount As Integer, ByVal Action As SwizzleType) As Byte()
    Dim swiz As New Swizzle(width, height, depth)
    Dim tmp1, tmp2 As Integer
    Dim bChunk() As Byte

    ReDim bChunk(bin.Length - 1)
    For y As Integer = 0 To height - 1
      For x As Integer = 0 To width - 1
        Select Case Action
          Case SwizzleType.DeSwizzle
            tmp1 = ((y * width) + x) * (BitCount / 8)
            tmp2 = (swiz.Swizzle(x, y, -1)) * (BitCount / 8)
          Case SwizzleType.Swizzle
            tmp2 = ((y * width) + x) * (BitCount / 8)
            tmp1 = (swiz.Swizzle(x, y, -1)) * (BitCount / 8)
        End Select
        For i As Integer = 0 To (BitCount / 8) - 1
          bChunk(tmp1 + i) = bin(tmp2 + i)
        Next
      Next
    Next
    Return bChunk
  End Function

  Public Function SwizzlePicture4Bit(ByVal bin() As Byte, ByVal width As Integer, ByVal height As Integer, _
ByVal depth As Integer, ByVal BitCount As Integer, ByVal Action As SwizzleType) As Byte()
    Dim swiz As New Swizzle(width, height, depth)
    Dim tmp1, tmp2 As Integer
    Dim bChunk() As Byte

    ReDim bChunk(bin.Length - 1)
    For y As Integer = 0 To height - 1
      For x As Integer = 0 To width - 1
        Select Case Action
          Case SwizzleType.DeSwizzle
            tmp1 = ((y * width) + x) * (BitCount / 4)
            tmp2 = (swiz.Swizzle(x, y, -1)) * (BitCount / 4)
          Case SwizzleType.Swizzle
            tmp2 = ((y * width) + x) * (BitCount / 4)
            tmp1 = (swiz.Swizzle(x, y, -1)) * (BitCount / 4)
        End Select
        For i As Integer = 0 To (BitCount / 4) - 1
          bChunk(tmp1 + i) = bin(tmp2 + i)
        Next
      Next
    Next
    Return bChunk
  End Function
#End Region

  Public Sub Read(ByVal stream As FileStream, ByVal metaoffset As Integer, ByVal magic As Long, ByVal filename As String, ByVal sharedstream As FileStream, ByVal mainmenustream As FileStream, ByVal spsharedstream As FileStream)
    ' Save it
    _stream = stream
    _filename = filename
    _shared = sharedstream
    _mainmenu = mainmenustream
    _spshared = spsharedstream

    ' Open reader
    Dim br As BinaryReader = New BinaryReader(_stream)
    bitmheader = New BitmapHeader

    ' Move to meta part
    br.BaseStream.Position = metaoffset

    ' Read header
    bitmheader.Read(br, magic)

    FillListBox()

    imagesListBox.SelectedIndex = 0
  End Sub

  Public Sub FillListBox()
    ' Clear current items
    imagesListBox.Items.Clear()

    ' Add items
    For i As Integer = 0 To bitmheader.Count - 1
      imagesListBox.Items.Add("Bitmap " & i & " (" & bitmheader._items(i).Width & "x" & bitmheader._items(i).Height & ")")
    Next
  End Sub

  Public Sub ShowBitmap(ByVal index As Integer)
    ' Show on screen
    partsTextBox.Text = bitmheader._items(index).Parts
    sizeTextBox.Text = bitmheader._items(index).TotalSize
    formatTextBox.Text = ImageType(bitmheader._items(index).Format)

    ' Show preview
    ShowPreview(index)
  End Sub

  Public Sub ExtractDDS(ByRef bw As BinaryWriter, Optional ByVal index As Integer = 0)
    Dim dds As New DDS_HEADER_STRUCTURE
    Dim br As BinaryReader = New BinaryReader(_stream)
    Dim bChunk() As Byte
    Dim tmp() As Byte
    Dim swiz As Swizzle

    Dim width, height As Integer

    Try
      width = bitmheader._items(index).Width
      height = bitmheader._items(index).Height

      If bitmheader._items(index).NeedsSpShared Then
        bChunk = bitmheader._items(index).ReadBin(_spshared)
      ElseIf bitmheader._items(index).NeedsShared Then
        bChunk = bitmheader._items(index).ReadBin(_shared)
      ElseIf bitmheader._items(index).NeedsMainmenu Then
        bChunk = bitmheader._items(index).ReadBin(_mainmenu)
      Else
        bChunk = bitmheader._items(index).ReadBin(_stream)
      End If

      Try
        If bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_A8R8G8B8 Or _
            bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_X8R8G8B8 Then _
            tmp = SwizzlePicture(bChunk, width, height, -1, 32, SwizzleType.Swizzle)
        If bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_A1R5G5B5 Or _
            bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_R5G6B5 Or _
            bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_A4R4G4B4 Then _
            tmp = SwizzlePicture(bChunk, width, height, -1, 16, SwizzleType.Swizzle)
        bChunk = tmp
      Catch
        MsgBox("Error in swizzling image, swizzle the picture yourself")
      End Try

      dds.generate(bitmheader._items(index))
      dds.writeStruct(bw)
      bw.Write(bChunk)
      bw.Close()

    Catch ex As Exception
      MsgBox(ex.Message, MsgBoxStyle.Information, "Error extracting image")
    End Try

  End Sub

  Public Function InjectDDS(ByVal br As BinaryReader, Optional ByVal index As Integer = 0) As String
    Dim bw As BinaryWriter
    If bitmheader._items(index).NeedsSpShared Then
      bw = New BinaryWriter(_spshared)
    ElseIf bitmheader._items(index).NeedsShared Then
      bw = New BinaryWriter(_shared)
    ElseIf bitmheader._items(index).NeedsMainmenu Then
      bw = New BinaryWriter(_mainmenu)
    Else
      bw = New BinaryWriter(_stream)
    End If
    Dim dds As DDS_HEADER_STRUCTURE
    Dim bChunk() As Byte

    ' Read dds header
    dds.readStruct(br)

    If (Not dds.magic = "DDS ") Then
      InjectDDS = "Invalid Header: " & vbCrLf & _
          "You must choose a valid DDS format texture."
      Exit Function
    End If

    'Check the structure to see if it matches the current sound.
    Dim bCompatible As Boolean = True
    Dim strErrorMsg As String

    'Make sure images are the same pixel format
    Select Case dds.ddsd.ddfPixelFormat.FourCC
      Case "DXT1"
        If Not bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_DXT1 Then
          bCompatible = False
          strErrorMsg &= "Image being injected must be DXT1." & vbCrLf
        End If
      Case "DXT2", "DXT3"
        If Not bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_DXT2AND3 Then
          bCompatible = False
          strErrorMsg &= "Image being injected must be DXT2 or DXT3." & vbCrLf
        End If
      Case "DXT4", "DXT5"
        If Not bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_DXT4AND5 Then
          bCompatible = False
          strErrorMsg &= "Image being injected must be DXT4 or DXT5." & vbCrLf
        End If
      Case Else
    End Select

    Dim width As Integer = bitmheader._items(index).Width
    Dim height As Integer = bitmheader._items(index).Height

    If bCompatible Then
      Try
        'Grab a chunk of the file.
        Dim tmp(bitmheader._items(index).TotalSize) As Byte

        bChunk = br.ReadBytes(bitmheader._items(index).TotalSize)
        If bChunk.Length < tmp.Length Then
          bChunk.CopyTo(tmp, 0)
          bChunk = tmp
        End If

        'Swizzle the chunk if neccessary
        If bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_A8R8G8B8 Or _
            bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_X8R8G8B8 Then _
            bChunk = SwizzlePicture(bChunk, width, height, -1, 32, SwizzleType.Swizzle)
        If bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_A1R5G5B5 Or _
          bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_R5G6B5 Or _
            bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_A4R4G4B4 Then _
            bChunk = SwizzlePicture(bChunk, width, height, -1, 16, SwizzleType.Swizzle)

        'Inject the chunk into the appropriate map offset
        bitmheader._items(index).WriteBin(bw, bChunk)

        'Replace the appropriate chunk buffer
        strErrorMsg = "Texture was successfully imported."
      Catch ex As Exception
        strErrorMsg = "An error occured while importing data: " & vbCrLf & _
        ex.Message
      End Try
      bw.Flush()
      _stream.Flush()
    End If
    br.Close()

    InjectDDS = strErrorMsg
  End Function

  Private Sub extractButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles extractButton.Click
    ExtractCurrent()
  End Sub

  Private Sub ExtractCurrent()
    ' Filename
    bitmapSaveFileDialog.FileName = _filename.Substring(_filename.LastIndexOf("\") + 1) + "." + imagesListBox.SelectedIndex.ToString + ".dds"

    If bitmapSaveFileDialog.ShowDialog = DialogResult.OK Then
      Dim fs As FileStream = New FileStream(bitmapSaveFileDialog.FileName, FileMode.Create, FileAccess.Write)
      Dim bw As BinaryWriter = New BinaryWriter(fs)

      ExtractDDS(bw, imagesListBox.SelectedIndex)
    End If
  End Sub

  Private Sub InjectCurrent()
    ' Filename
    If bitmapOpenFileDialog.ShowDialog = DialogResult.OK Then
      Dim fs As FileStream = New FileStream(bitmapOpenFileDialog.FileName, FileMode.Open, FileAccess.Read)
      Dim br As BinaryReader = New BinaryReader(fs)

      MsgBox(InjectDDS(br, imagesListBox.SelectedIndex))

      ShowPreview(imagesListBox.SelectedIndex)
    End If
  End Sub

  Private Sub imagesListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imagesListBox.SelectedIndexChanged
    ShowBitmap(imagesListBox.SelectedIndex)
  End Sub

  Public Sub ShowPreview(ByVal index As Integer)
    Dim l As New ImageLib
    Dim ImageSize As Integer
    Dim bSupported As Boolean = False
    Dim bShowPreview As Boolean = True
    Dim bError As Boolean = False

    ' Check if image is supported
    If bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_DXT1 Or _
        bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_DXT2AND3 Or _
        bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_DXT4AND5 Or _
        bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_A1R5G5B5 Or _
        bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_A4R4G4B4 Or _
        bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_A8R8G8B8 Or _
        bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_R5G6B5 Or _
        bitmheader._items(index).Format = bitmEnum.BITM_FORMAT_X8R8G8B8 Then
      bSupported = True
    Else
      bSupported = False
    End If

    injectButton.Enabled = bSupported

    ImageSize = CLng(bitmheader._items(index).Width) * CLng(bitmheader._items(index).Height) * 4
    ReDim decodedChunk(ImageSize)
    Dim swiz As Swizzle
    Dim width As Integer = bitmheader._items(index).Width
    Dim height As Integer = bitmheader._items(index).Height
    Dim img As New ImageLib
    Dim preview As String
    Dim bChunk() As Byte

    Try
      ' Read raw data
      If bitmheader._items(index).NeedsSpShared Then
        bChunk = bitmheader._items(index).ReadBin(_spshared)
      ElseIf bitmheader._items(index).NeedsShared Then
        bChunk = bitmheader._items(index).ReadBin(_shared)
      ElseIf bitmheader._items(index).NeedsMainmenu Then
        bChunk = bitmheader._items(index).ReadBin(_mainmenu)
      Else
        bChunk = bitmheader._items(index).ReadBin(_stream)
      End If

      ' Check the format
      Select Case bitmheader._items(index).Format
        Case bitmEnum.BITM_FORMAT_DXT1
          decodedChunk = l.DecodeDXT1(height, width, bChunk)
        Case bitmEnum.BITM_FORMAT_DXT2AND3
          decodedChunk = l.DecodeDXT23(height, width, bChunk)
        Case bitmEnum.BITM_FORMAT_DXT4AND5
          decodedChunk = l.DecodeDXT45(height, width, bChunk)
        Case bitmEnum.BITM_FORMAT_A8R8G8B8, bitmEnum.BITM_FORMAT_X8R8G8B8, bitmEnum.BITM_FORMAT_R5G6B5
          Try
            decodedChunk = SwizzlePicture(bChunk, width, height, -1, 32, SwizzleType.DeSwizzle)
          Catch
            bError = True
            decodedChunk = bChunk
          End Try
        Case bitmEnum.BITM_FORMAT_A4R4G4B4
          decodedChunk = SwizzlePicture4Bit(bChunk, width, height, -1, 16, SwizzleType.DeSwizzle)
        Case Else
          ReDim decodedChunk(64 * 64 * 4)
          ImageSize = (64 * 64 * 4)
          height = 64
          width = 64
          bShowPreview = False
      End Select
    Catch
      bError = True
      bShowPreview = False
    End Try

    'Reserve a blobk of unmanaged memory for the byte array
    Marshal.FreeHGlobal(ptr)
    ptr = Marshal.AllocHGlobal(ImageSize)

    'Copy the decoded byte array to unmanaged memory
    Dim api As New WINAPI
    api.CopyMemory(ptr, decodedChunk, ImageSize)

    'Create the bitmap object
    b = New System.Drawing.Bitmap(width, height, width * 4, Imaging.PixelFormat.Format32bppArgb, ptr)
    If Not bSupported Then
      preview = "Not Supported "
    Else
      If Not bShowPreview Then
        preview += "No Preview Available "
      End If
      If bError Then
        preview += "(Error) "
      End If
    End If
    If bitmheader._items(index).NeedsSpShared Then
      preview += "(SP Shared)"
    ElseIf bitmheader._items(index).NeedsShared Then
      preview += "(Shared)"
    ElseIf bitmheader._items(index).NeedsMainmenu Then
      preview += "(Main Menu)"
    Else
      preview += "(Local)"
    End If
    previewLabel.Text = preview
    previewPictureBox.Image = b 'The picture box displays the bitmap
  End Sub

#Region "Image Library"
  Class ImageLib
    Public Structure RGBA_COLOR_STRUCT
      Public r, g, b, a As Integer
    End Structure
    Public Function Unsigned(ByVal i As Integer) As Long
      Unsigned = Val("&H" & Hex(i))
      If Unsigned < 0 Then
        Unsigned = Unsigned + (2 ^ 32)
      End If
    End Function
    Public Function short_to_rgba(ByVal color As Integer) As RGBA_COLOR_STRUCT
      Dim rc As RGBA_COLOR_STRUCT
      Dim clr As Long = Unsigned(color)
      rc.r = (((clr >> 11) And &H1F) * &HFF) / 31
      rc.g = (((clr >> 5) And &H3F) * &HFF) / 63
      rc.b = (((clr >> 0) And &H1F) * &HFF) / 31
      rc.a = 255
      Return rc
    End Function

    Public Function rgba_to_int(ByVal c As RGBA_COLOR_STRUCT) As Integer
      Return (c.a << 24) Or (c.r << 16) Or (c.g << 8) Or c.b
    End Function

    Public Function GradientColors(ByVal Col1 As RGBA_COLOR_STRUCT, _
        ByVal Col2 As RGBA_COLOR_STRUCT) As RGBA_COLOR_STRUCT
      Dim ret As RGBA_COLOR_STRUCT
      ret.r = ((Col1.r * 2 + Col2.r)) / 3
      ret.g = ((Col1.g * 2 + Col2.g)) / 3
      ret.b = ((Col1.b * 2 + Col2.b)) / 3
      ret.a = 255
      Return ret
    End Function

    Public Function GradientColorsHalf(ByVal Col1 As RGBA_COLOR_STRUCT, _
        ByVal Col2 As RGBA_COLOR_STRUCT) As RGBA_COLOR_STRUCT
      Dim ret As RGBA_COLOR_STRUCT
      ret.r = (Col1.r / 2 + Col2.r / 2)
      ret.g = (Col1.g / 2 + Col2.g / 2)
      ret.b = (Col1.b / 2 + Col2.b / 2)
      ret.a = 255
      Return ret
    End Function

    '//////////////////////////
    '// DecodeDXT1 
    '//////////////////////////
    Public Function DecodeDXT1(ByVal height As Integer, ByVal width As Integer, _
        ByVal SourceData() As Byte) As Byte()

      Dim DestData() As Byte
      Dim Color(4) As RGBA_COLOR_STRUCT
      Dim i As Integer
      Dim dptr As Integer
      Dim CColor As RGBA_COLOR_STRUCT
      Dim CData As Integer
      Dim ChunksPerHLine As Integer = width / 4
      Dim trans As Boolean
      Dim zeroColor As RGBA_COLOR_STRUCT
      Dim c1, c2 As Integer

      ReDim DestData(((width * height) * 4) - 1)

      If ChunksPerHLine = 0 Then ChunksPerHLine += 1

      For i = 0 To (width * height) - 1 Step 16

        c1 = (CInt(SourceData(dptr + 1)) << 8) Or (SourceData(dptr))
        c2 = (CInt(SourceData(dptr + 3)) << 8) Or (SourceData(dptr + 2))

        If c1 > c2 Then
          trans = False
        Else
          trans = True
        End If

        Color(0) = short_to_rgba(c1)
        Color(1) = short_to_rgba(c2)
        If Not trans Then
          Color(2) = GradientColors(Color(0), Color(1))
          Color(3) = GradientColors(Color(1), Color(0))
        Else
          Color(2) = GradientColorsHalf(Color(0), Color(1))
          Color(3) = zeroColor
        End If

        CData = (CInt(SourceData(dptr + 4)) << 0) Or _
            (CInt(SourceData(dptr + 5)) << 8) Or _
            (CInt(SourceData(dptr + 6)) << 16) Or _
            (CInt(SourceData(dptr + 7)) << 24)

        Dim ChunkNum As Integer = i / 16
        Dim XPos As Long = ChunkNum Mod ChunksPerHLine
        Dim YPos As Long = (ChunkNum - XPos) / ChunksPerHLine
        Dim ttmp As Long
        Dim ttmp2 As Long

        Dim sizeh As Integer = IIf(height < 4, height, 4)
        Dim sizew As Integer = IIf(width < 4, width, 4)
        Dim tStr As String
        For x As Integer = 0 To sizeh - 1
          For y As Integer = 0 To sizew - 1
            CColor = Color(CData And &H3)
            CData >>= 2
            ttmp = ((YPos * 4 + x) * width + XPos * 4 + y) * 4
            ttmp2 = rgba_to_int(CColor)
            DestData(ttmp) = CColor.b
            DestData(ttmp + 1) = CColor.g
            DestData(ttmp + 2) = CColor.r
            DestData(ttmp + 3) = CColor.a
          Next
        Next
        dptr += 8
      Next
      Return DestData
    End Function
    '//////////////////////////
    '// DecodeDXT2/3
    '//////////////////////////
    Public Function DecodeDXT23(ByVal height As Integer, ByVal width As Integer, _
        ByVal SourceData() As Byte) As Byte()

      Dim DestData() As Byte
      Dim Color(4) As RGBA_COLOR_STRUCT
      Dim i As Integer
      Dim CColor As RGBA_COLOR_STRUCT
      Dim CData As Integer
      Dim ChunksPerHLine As Integer = width / 4
      Dim trans As Boolean
      Dim zeroColor As RGBA_COLOR_STRUCT
      Dim c1, c2 As Integer

      ReDim DestData(((width * height) * 4) - 1)

      If ChunksPerHLine = 0 Then ChunksPerHLine += 1

      For i = 0 To (width * height) - 1 Step 16

        Color(0) = short_to_rgba(CInt(SourceData(i + 8)) Or CInt(SourceData(i + 9)) << 8)
        Color(1) = short_to_rgba(CInt(SourceData(i + 10)) Or CInt(SourceData(i + 11)) << 8)
        Color(2) = GradientColors(Color(0), Color(1))
        Color(3) = GradientColors(Color(1), Color(0))

        CData = (CInt(SourceData(i + 12)) << 0) Or _
            (CInt(SourceData(i + 13)) << 8) Or _
            (CInt(SourceData(i + 14)) << 16) Or _
            (CInt(SourceData(i + 15)) << 24)

        Dim ChunkNum As Integer = i / 16
        Dim XPos As Long = ChunkNum Mod ChunksPerHLine
        Dim YPos As Long = (ChunkNum - XPos) / ChunksPerHLine
        Dim ttmp As Long
        Dim ttmp2 As Long
        Dim alpha As Integer

        Dim sizeh As Integer = IIf(height < 4, height, 4)
        Dim sizew As Integer = IIf(width < 4, width, 4)
        Dim tStr As String
        For x As Integer = 0 To sizeh - 1
          alpha = SourceData(i + (2 * x)) Or CInt(SourceData(i + (2 * x) + 1)) << 8
          For y As Integer = 0 To sizew - 1
            CColor = Color(CData And &H3)
            CData >>= 2
            CColor.a = (alpha And &HF) * 16
            alpha >>= 4
            ttmp = ((YPos * 4 + x) * width + XPos * 4 + y) * 4

            DestData(ttmp) = CColor.b
            DestData(ttmp + 1) = CColor.g
            DestData(ttmp + 2) = CColor.r
            DestData(ttmp + 3) = CColor.a
          Next
        Next
      Next
      Return DestData
    End Function
    '//////////////////////////
    '// DecodeDXT4/5
    '//////////////////////////
    Public Function DecodeDXT45(ByVal height As Integer, ByVal width As Integer, _
        ByVal SourceData() As Byte) As Byte()

      Dim DestData() As Byte
      Dim Color(4) As RGBA_COLOR_STRUCT
      Dim i As Integer
      Dim CColor As RGBA_COLOR_STRUCT
      Dim CData As Integer
      Dim ChunksPerHLine As Integer = width / 4
      Dim trans As Boolean
      Dim zeroColor As RGBA_COLOR_STRUCT
      Dim c1, c2 As Integer

      ReDim DestData(((width * height) * 4) - 1)

      If ChunksPerHLine = 0 Then ChunksPerHLine += 1

      For i = 0 To (width * height) - 1 Step 16

        Color(0) = short_to_rgba(CInt(SourceData(i + 8)) Or CInt(SourceData(i + 9)) << 8)
        Color(1) = short_to_rgba(CInt(SourceData(i + 10)) Or CInt(SourceData(i + 11)) << 8)
        Color(2) = GradientColors(Color(0), Color(1))
        Color(3) = GradientColors(Color(1), Color(0))

        CData = (CInt(SourceData(i + 12)) << 0) Or _
            (CInt(SourceData(i + 13)) << 8) Or _
            (CInt(SourceData(i + 14)) << 16) Or _
            (CInt(SourceData(i + 15)) << 24)

        Dim Alpha(8) As Byte

        Alpha(0) = SourceData(i)
        Alpha(1) = SourceData(i + 1)

        'Do the alphas
        If (Alpha(0) > Alpha(1)) Then
          '// 8-alpha block:  derive the other six alphas.
          '// Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.
          Alpha(2) = (6 * Alpha(0) + 1 * Alpha(1) + 3) / 7 '// bit code 010
          Alpha(3) = (5 * Alpha(0) + 2 * Alpha(1) + 3) / 7 '// bit code 011
          Alpha(4) = (4 * Alpha(0) + 3 * Alpha(1) + 3) / 7 '// bit code 100
          Alpha(5) = (3 * Alpha(0) + 4 * Alpha(1) + 3) / 7 '// bit code 101
          Alpha(6) = (2 * Alpha(0) + 5 * Alpha(1) + 3) / 7 '// bit code 110
          Alpha(7) = (1 * Alpha(0) + 6 * Alpha(1) + 3) / 7 '// bit code 111
        Else
          '// 6-alpha block.
          '// Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.
          Alpha(2) = (4 * Alpha(0) + 1 * Alpha(1) + 2) / 5 '// Bit code 010
          Alpha(3) = (3 * Alpha(0) + 2 * Alpha(1) + 2) / 5 '// Bit code 011
          Alpha(4) = (2 * Alpha(0) + 3 * Alpha(1) + 2) / 5 '// Bit code 100
          Alpha(5) = (1 * Alpha(0) + 4 * Alpha(1) + 2) / 5 '// Bit code 101
          Alpha(6) = 0            '// Bit code 110
          Alpha(7) = 255          '// Bit code 111
        End If

        '// Byte	Alpha
        '// 0	Alpha_0
        '// 1	Alpha_1 
        '// 2	(0)(2) (2 LSBs), (0)(1), (0)(0)
        '// 3	(1)(1) (1 LSB), (1)(0), (0)(3), (0)(2) (1 MSB)
        '// 4	(1)(3), (1)(2), (1)(1) (2 MSBs)
        '// 5	(2)(2) (2 LSBs), (2)(1), (2)(0)
        '// 6	(3)(1) (1 LSB), (3)(0), (2)(3), (2)(2) (1 MSB)
        '// 7	(3)(3), (3)(2), (3)(1) (2 MSBs)
        '// (0

        '// Read an int and a short
        Dim tmpdword As Long
        Dim tmpword As Integer
        Dim alphaDat As Long

        tmpword = SourceData(i + 2) Or (CInt(SourceData(i + 3)) << 8)
        tmpdword = SourceData(i + 4) Or (CInt(SourceData(i + 5)) << 8) Or (SourceData(i + 6) << 16) Or (CInt(SourceData(i + 7)) << 24)

        alphaDat = tmpword Or (tmpdword << 16)

        Dim ChunkNum As Integer = i / 16
        Dim XPos As Long = ChunkNum Mod ChunksPerHLine
        Dim YPos As Long = (ChunkNum - XPos) / ChunksPerHLine
        Dim ttmp As Long
        Dim ttmp2 As Long

        Dim sizeh As Integer = IIf(height < 4, height, 4)
        Dim sizew As Integer = IIf(width < 4, width, 4)
        Dim tStr As String
        For x As Integer = 0 To sizeh - 1
          For y As Integer = 0 To sizew - 1
            CColor = Color(CData And &H3)
            CData >>= 2
            CColor.a = Alpha(alphaDat And &H7)
            alphaDat >>= 3
            ttmp = ((YPos * 4 + x) * width + XPos * 4 + y) * 4

            DestData(ttmp) = CColor.b
            DestData(ttmp + 1) = CColor.g
            DestData(ttmp + 2) = CColor.r
            DestData(ttmp + 3) = CColor.a
          Next
        Next
      Next
      Return DestData
    End Function
    Public Function Overlay(ByVal width As Integer, ByVal height As Integer, ByVal img As Image, ByVal OverlayText As String, ByVal OverlayFont As Font, _
        ByVal OverlayColor As Color, ByVal AddAlpha As Boolean, ByVal AddShadow As Boolean) As Bitmap
      If OverlayText > "" Then
        ' create bitmap and graphics used for drawing
        Dim bmp As New Bitmap(img)
        Dim g As Graphics = Graphics.FromImage(bmp)

        Dim alpha As Integer = 255
        If AddAlpha Then
          ' Compute transparency: Longer text should be less transparent or it gets lost.
          alpha = 90 + (OverlayText.Length * 2)
          If alpha >= 255 Then alpha = 255
        End If
        ' Create the brush based on the color and alpha
        Dim b As New SolidBrush(Color.FromArgb(alpha, OverlayColor))

        ' Measure the text to render (unscaled, unwrapped)
        Dim s As SizeF = g.MeasureString(OverlayText, OverlayFont)

        ' Enlarge font to ~80% fill (estimated by AREA)
        Dim zoom As Single = CSng(Math.Sqrt((CDbl(img.Width * img.Height) * 0.8) / CDbl(s.Width * s.Height)))
        Dim sty As FontStyle = OverlayFont.Style
        Dim f As New Font(OverlayFont.FontFamily, CSng(OverlayFont.Size) * zoom, sty)
        Console.WriteLine("Starting Zoom: " & zoom & ", Font Size: " & f.Size & ", Alpha: " & alpha)

        ' Measure using new font size, allow to wrap as needed.
        ' Could rotate the overlay at a 30-45 deg. angle (trig would give correct angle).
        ' Of course, then the area covered would be less than "straight" text.
        ' I'll leave those calculations for someone else....
        Dim strFormat As New StringFormat
        Dim charFit, linesFit As Integer
        strFormat.FormatFlags = StringFormatFlags.NoClip 'Or StringFormatFlags.LineLimit 'Or StringFormatFlags.MeasureTrailingSpaces
        strFormat.Trimming = StringTrimming.Word
        Dim layout As New SizeF(CSng(width) * 0.9!, CSng(height) * 1.5!) ' fit to width, allow height to go over
        Console.WriteLine("Target size: " & layout.Width & ", " & layout.Height)
        s = g.MeasureString(OverlayText, f, layout, strFormat, charFit, linesFit)

        ' Reduce size until it actually fits...
        ' Most text only has to be reduced 1 or 2 times.
        Do Until s.Height <= CSng(height) * 0.9! AndAlso s.Width <= layout.Width
          Console.WriteLine("Reducing font size: area required = " & s.Width & ", " & s.Height)
          zoom = Math.Max(s.Height / (CSng(height) * 0.9!), s.Width / layout.Width)
          zoom = f.Size / zoom
          If zoom > 16 Then zoom = CSng(Math.Floor(zoom)) ' use a whole number to reduce "jaggies"
          If zoom >= f.Size Then zoom -= 1
          f = New Font(OverlayFont.FontFamily, zoom, sty)
          s = g.MeasureString(OverlayText, f, layout, strFormat, charFit, linesFit)
          If zoom <= 1 Then Exit Do ' bail
        Loop
        Console.WriteLine("Final Font Size: " & f.Size & ", area: " & s.Width & ", " & s.Height)

        ' Determine draw area (centered)
        Dim rect As New RectangleF((bmp.Width - s.Width) / 2, (bmp.Height - s.Height) / 2, layout.Width, CSng(width) * 0.9!)

        If AddShadow Then
          ' Add "drop shadow" at half transparency and offset by 1/10 font size
          Dim shadow As New SolidBrush(Color.FromArgb(CInt(alpha / 2), OverlayColor))
          Dim sRect As New RectangleF(rect.X - (f.Size * 0.1!), rect.Y - (f.Size * 0.1!), rect.Width, rect.Height)
          g.DrawString(OverlayText, f, shadow, sRect, strFormat)
        End If

        ' Finally, draw centered text!
        g.DrawString(OverlayText, f, b, rect, strFormat)

        ' clean-up
        g.Dispose()
        b.Dispose()
        f.Dispose()
        Return bmp
      Else
        ' nothing to overlay!
        Return New Bitmap(img)
      End If
    End Function
  End Class
#End Region

  Public Class WINAPI
    ' Declare the Windows API function CopyMemory
    ' Note that all variables are ByVal. pDst is passed ByVal because we want
    ' CopyMemory to go to that location and modify the data that is pointed to
    ' by the IntPtr, and not the pointer itself.                   
    Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal pDst As IntPtr, _
                                                                ByVal pSrc() As Byte, _
                                                                ByVal ByteLen As Long)
    Declare Sub PlaySound Lib "winmm.dll" Alias "PlaySoundA" (ByVal data() As Byte, _
                                                              ByVal hMod As IntPtr, _
                                                              ByVal dwFlags As Int32)
  End Class

  Private Sub imagesListBox_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles imagesListBox.DoubleClick
    ExtractCurrent()
  End Sub

  Private Sub injectButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles injectButton.Click
    InjectCurrent()
  End Sub
End Class
