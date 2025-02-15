Public Class CustomDesignForm
    Inherits DevExpress.XtraReports.UserDesigner.XRDesignFormExBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        'Dim item As New XtraPrintingDemos.BarLookAndFeelListItem(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        'XrDesignBarManager1.Items.Add(item)
        'bsiLookAndFeel.AddItem(item)
    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents XrDesignBarManager1 As DevExpress.XtraReports.UserDesigner.XRDesignBarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents ricbFontName As DevExpress.XtraReports.UserDesigner.RecentlyUsedItemsComboBox
    Friend WithEvents ricbFontSize As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents DesignBar1 As DevExpress.XtraReports.UserDesigner.DesignBar
    Friend WithEvents DesignBar2 As DevExpress.XtraReports.UserDesigner.DesignBar
    Friend WithEvents DesignBar3 As DevExpress.XtraReports.UserDesigner.DesignBar
    Friend WithEvents DesignBar4 As DevExpress.XtraReports.UserDesigner.DesignBar
    Friend WithEvents DesignBar5 As DevExpress.XtraReports.UserDesigner.DesignBar
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents BarEditItem2 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents CommandBarItem1 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem2 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem3 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandColorBarItem1 As DevExpress.XtraReports.UserDesigner.CommandColorBarItem
    Friend WithEvents CommandColorBarItem2 As DevExpress.XtraReports.UserDesigner.CommandColorBarItem
    Friend WithEvents CommandBarItem4 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem5 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem6 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem7 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem8 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem9 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem10 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem11 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem12 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem13 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem14 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem15 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem16 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem17 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem18 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem19 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem20 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem21 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem22 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem23 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem24 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem25 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem26 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem27 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem28 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem29 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem30 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem31 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem32 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem33 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem34 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem35 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem36 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem37 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem38 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem2 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem3 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarReportTabButtonsListItem1 As DevExpress.XtraReports.UserDesigner.BarReportTabButtonsListItem
    Friend WithEvents BarSubItem4 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents XrBarToolbarsListItem1 As DevExpress.XtraReports.UserDesigner.XRBarToolbarsListItem
    Friend WithEvents BarSubItem5 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarDockPanelsListItem1 As DevExpress.XtraReports.UserDesigner.BarDockPanelsListItem
    Friend WithEvents BarSubItem6 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem7 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem8 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem9 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem10 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem11 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem12 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem13 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem14 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents CommandBarItem39 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem40 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem41 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem42 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents CommandBarItem43 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents bsiLookAndFeel As DevExpress.XtraBars.BarSubItem
    Friend WithEvents XrDesignDockManager1 As DevExpress.XtraReports.UserDesigner.XRDesignDockManager
    Friend WithEvents DesignRepositoryItemComboBox1 As DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox
    Friend WithEvents DesignBar6 As DevExpress.XtraReports.UserDesigner.DesignBar
    Friend WithEvents CommandBarItem44 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    Friend WithEvents XrZoomBarEditItem1 As DevExpress.XtraReports.UserDesigner.XRZoomBarEditItem
    Friend WithEvents GroupAndSortDockPanel1 As DevExpress.XtraReports.UserDesigner.GroupAndSortDockPanel
    Friend WithEvents GroupAndSortDockPanel1_Container As DevExpress.XtraReports.UserDesigner.DesignControlContainer
    Friend WithEvents ToolBoxDockPanel1 As DevExpress.XtraReports.UserDesigner.ToolBoxDockPanel
    Friend WithEvents ToolBoxDockPanel1_Container As DevExpress.XtraReports.UserDesigner.DesignControlContainer
    Friend WithEvents panelContainer1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents panelContainer2 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ReportExplorerDockPanel1 As DevExpress.XtraReports.UserDesigner.ReportExplorerDockPanel
    Friend WithEvents ReportExplorerDockPanel1_Container As DevExpress.XtraReports.UserDesigner.DesignControlContainer
    Friend WithEvents FieldListDockPanel1 As DevExpress.XtraReports.UserDesigner.FieldListDockPanel
    Friend WithEvents FieldListDockPanel1_Container As DevExpress.XtraReports.UserDesigner.DesignControlContainer
    Friend WithEvents PropertyGridDockPanel1 As DevExpress.XtraReports.UserDesigner.PropertyGridDockPanel
    Friend WithEvents PropertyGridDockPanel1_Container As DevExpress.XtraReports.UserDesigner.DesignControlContainer
    Friend WithEvents hideContainerLeft As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents CommandBarItem45 As DevExpress.XtraReports.UserDesigner.CommandBarItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomDesignForm))
        Me.XrDesignBarManager1 = New DevExpress.XtraReports.UserDesigner.XRDesignBarManager
        Me.DesignBar1 = New DevExpress.XtraReports.UserDesigner.DesignBar
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem31 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem39 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem32 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem33 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem40 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem41 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.BarSubItem2 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem37 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem38 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem34 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem35 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem36 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem42 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem43 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.BarSubItem3 = New DevExpress.XtraBars.BarSubItem
        Me.BarReportTabButtonsListItem1 = New DevExpress.XtraReports.UserDesigner.BarReportTabButtonsListItem
        Me.BarSubItem4 = New DevExpress.XtraBars.BarSubItem
        Me.XrBarToolbarsListItem1 = New DevExpress.XtraReports.UserDesigner.XRBarToolbarsListItem
        Me.BarSubItem5 = New DevExpress.XtraBars.BarSubItem
        Me.BarDockPanelsListItem1 = New DevExpress.XtraReports.UserDesigner.BarDockPanelsListItem
        Me.BarSubItem6 = New DevExpress.XtraBars.BarSubItem
        Me.CommandColorBarItem1 = New DevExpress.XtraReports.UserDesigner.CommandColorBarItem
        Me.CommandColorBarItem2 = New DevExpress.XtraReports.UserDesigner.CommandColorBarItem
        Me.BarSubItem7 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem1 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem2 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem3 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.BarSubItem8 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem4 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem5 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem6 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem7 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.BarSubItem9 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem9 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem10 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem11 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem12 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem13 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem14 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem8 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.BarSubItem10 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem15 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem16 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem17 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem18 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.BarSubItem11 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem19 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem20 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem21 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem22 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.BarSubItem12 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem23 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem24 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem25 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem26 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.BarSubItem13 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem27 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem28 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.BarSubItem14 = New DevExpress.XtraBars.BarSubItem
        Me.CommandBarItem29 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.CommandBarItem30 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.bsiLookAndFeel = New DevExpress.XtraBars.BarSubItem
        Me.DesignBar2 = New DevExpress.XtraReports.UserDesigner.DesignBar
        Me.DesignBar3 = New DevExpress.XtraReports.UserDesigner.DesignBar
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem
        Me.ricbFontName = New DevExpress.XtraReports.UserDesigner.RecentlyUsedItemsComboBox
        Me.BarEditItem2 = New DevExpress.XtraBars.BarEditItem
        Me.ricbFontSize = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
        Me.DesignBar4 = New DevExpress.XtraReports.UserDesigner.DesignBar
        Me.DesignBar5 = New DevExpress.XtraReports.UserDesigner.DesignBar
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem
        Me.DesignBar6 = New DevExpress.XtraReports.UserDesigner.DesignBar
        Me.CommandBarItem44 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.XrZoomBarEditItem1 = New DevExpress.XtraReports.UserDesigner.XRZoomBarEditItem
        Me.DesignRepositoryItemComboBox1 = New DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox
        Me.CommandBarItem45 = New DevExpress.XtraReports.UserDesigner.CommandBarItem
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
        Me.XrDesignDockManager1 = New DevExpress.XtraReports.UserDesigner.XRDesignDockManager
        Me.hideContainerLeft = New DevExpress.XtraBars.Docking.AutoHideContainer
        Me.ToolBoxDockPanel1 = New DevExpress.XtraReports.UserDesigner.ToolBoxDockPanel
        Me.ToolBoxDockPanel1_Container = New DevExpress.XtraReports.UserDesigner.DesignControlContainer
        Me.panelContainer1 = New DevExpress.XtraBars.Docking.DockPanel
        Me.panelContainer2 = New DevExpress.XtraBars.Docking.DockPanel
        Me.ReportExplorerDockPanel1 = New DevExpress.XtraReports.UserDesigner.ReportExplorerDockPanel
        Me.ReportExplorerDockPanel1_Container = New DevExpress.XtraReports.UserDesigner.DesignControlContainer
        Me.FieldListDockPanel1 = New DevExpress.XtraReports.UserDesigner.FieldListDockPanel
        Me.FieldListDockPanel1_Container = New DevExpress.XtraReports.UserDesigner.DesignControlContainer
        Me.PropertyGridDockPanel1 = New DevExpress.XtraReports.UserDesigner.PropertyGridDockPanel
        Me.PropertyGridDockPanel1_Container = New DevExpress.XtraReports.UserDesigner.DesignControlContainer
        Me.GroupAndSortDockPanel1 = New DevExpress.XtraReports.UserDesigner.GroupAndSortDockPanel
        Me.GroupAndSortDockPanel1_Container = New DevExpress.XtraReports.UserDesigner.DesignControlContainer
        CType(Me.xrDesignPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrDesignBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ricbFontName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ricbFontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DesignRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrDesignDockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerLeft.SuspendLayout()
        Me.ToolBoxDockPanel1.SuspendLayout()
        Me.panelContainer1.SuspendLayout()
        Me.panelContainer2.SuspendLayout()
        Me.ReportExplorerDockPanel1.SuspendLayout()
        Me.FieldListDockPanel1.SuspendLayout()
        Me.PropertyGridDockPanel1.SuspendLayout()
        Me.GroupAndSortDockPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'xrDesignPanel
        '
        Me.xrDesignPanel.Location = New System.Drawing.Point(22, 78)
        Me.xrDesignPanel.ShowComponentTray = False
        Me.xrDesignPanel.Size = New System.Drawing.Size(565, 333)
        '
        'XrDesignBarManager1
        '
        Me.XrDesignBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.DesignBar1, Me.DesignBar2, Me.DesignBar3, Me.DesignBar4, Me.DesignBar5, Me.DesignBar6})
        Me.XrDesignBarManager1.DockControls.Add(Me.barDockControlTop)
        Me.XrDesignBarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.XrDesignBarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.XrDesignBarManager1.DockControls.Add(Me.barDockControlRight)
        Me.XrDesignBarManager1.DockManager = Me.XrDesignDockManager1
        Me.XrDesignBarManager1.FontNameBox = Me.ricbFontName
        Me.XrDesignBarManager1.FontNameEdit = Me.BarEditItem1
        Me.XrDesignBarManager1.FontSizeBox = Me.ricbFontSize
        Me.XrDesignBarManager1.FontSizeEdit = Me.BarEditItem2
        Me.XrDesignBarManager1.Form = Me
        Me.XrDesignBarManager1.FormattingToolbar = Me.DesignBar3
        Me.XrDesignBarManager1.HintStaticItem = Me.BarStaticItem1
        Me.XrDesignBarManager1.ImageStream = CType(resources.GetObject("XrDesignBarManager1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.XrDesignBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarEditItem1, Me.BarEditItem2, Me.CommandBarItem1, Me.CommandBarItem2, Me.CommandBarItem3, Me.CommandColorBarItem1, Me.CommandColorBarItem2, Me.CommandBarItem4, Me.CommandBarItem5, Me.CommandBarItem6, Me.CommandBarItem7, Me.CommandBarItem8, Me.CommandBarItem9, Me.CommandBarItem10, Me.CommandBarItem11, Me.CommandBarItem12, Me.CommandBarItem13, Me.CommandBarItem14, Me.CommandBarItem15, Me.CommandBarItem16, Me.CommandBarItem17, Me.CommandBarItem18, Me.CommandBarItem19, Me.CommandBarItem20, Me.CommandBarItem21, Me.CommandBarItem22, Me.CommandBarItem23, Me.CommandBarItem24, Me.CommandBarItem25, Me.CommandBarItem26, Me.CommandBarItem27, Me.CommandBarItem28, Me.CommandBarItem29, Me.CommandBarItem30, Me.CommandBarItem31, Me.CommandBarItem32, Me.CommandBarItem33, Me.CommandBarItem34, Me.CommandBarItem35, Me.CommandBarItem36, Me.CommandBarItem37, Me.CommandBarItem38, Me.BarStaticItem1, Me.BarSubItem1, Me.BarSubItem2, Me.BarSubItem3, Me.BarReportTabButtonsListItem1, Me.BarSubItem4, Me.XrBarToolbarsListItem1, Me.BarSubItem5, Me.BarDockPanelsListItem1, Me.BarSubItem6, Me.BarSubItem7, Me.BarSubItem8, Me.BarSubItem9, Me.BarSubItem10, Me.BarSubItem11, Me.BarSubItem12, Me.BarSubItem13, Me.BarSubItem14, Me.CommandBarItem39, Me.CommandBarItem40, Me.CommandBarItem41, Me.CommandBarItem42, Me.CommandBarItem43, Me.bsiLookAndFeel, Me.CommandBarItem44, Me.XrZoomBarEditItem1, Me.CommandBarItem45})
        Me.XrDesignBarManager1.LayoutToolbar = Me.DesignBar4
        Me.XrDesignBarManager1.MainMenu = Me.DesignBar1
        Me.XrDesignBarManager1.MaxItemId = 69
        Me.XrDesignBarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ricbFontName, Me.ricbFontSize, Me.DesignRepositoryItemComboBox1})
        Me.XrDesignBarManager1.StatusBar = Me.DesignBar5
        Me.XrDesignBarManager1.Toolbar = Me.DesignBar2
        Me.XrDesignBarManager1.XRDesignPanel = Me.xrDesignPanel
        Me.XrDesignBarManager1.ZoomItem = Me.XrZoomBarEditItem1
        '
        'DesignBar1
        '
        Me.DesignBar1.BarName = "MainMenu"
        Me.DesignBar1.DockCol = 0
        Me.DesignBar1.DockRow = 0
        Me.DesignBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.DesignBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem6), New DevExpress.XtraBars.LinkPersistInfo(Me.bsiLookAndFeel)})
        Me.DesignBar1.OptionsBar.MultiLine = True
        Me.DesignBar1.OptionsBar.UseWholeRow = True
        Me.DesignBar1.Text = "Main Menu"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "&File"
        Me.BarSubItem1.Id = 43
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem31), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem39), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem32), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem33, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem40), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem41, True)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'CommandBarItem31
        '
        Me.CommandBarItem31.Caption = "&New"
        Me.CommandBarItem31.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.NewReport
        Me.CommandBarItem31.Hint = "Create a new blank report"
        Me.CommandBarItem31.Id = 34
        Me.CommandBarItem31.ImageIndex = 9
        Me.CommandBarItem31.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
        Me.CommandBarItem31.Name = "CommandBarItem31"
        '
        'CommandBarItem39
        '
        Me.CommandBarItem39.Caption = "New with &Wizard..."
        Me.CommandBarItem39.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.NewReportWizard
        Me.CommandBarItem39.Hint = "Create a new report using the Wizard"
        Me.CommandBarItem39.Id = 60
        Me.CommandBarItem39.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W))
        Me.CommandBarItem39.Name = "CommandBarItem39"
        '
        'CommandBarItem32
        '
        Me.CommandBarItem32.Caption = "&Open..."
        Me.CommandBarItem32.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.OpenFile
        Me.CommandBarItem32.Hint = "Open a report"
        Me.CommandBarItem32.Id = 35
        Me.CommandBarItem32.ImageIndex = 10
        Me.CommandBarItem32.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
        Me.CommandBarItem32.Name = "CommandBarItem32"
        '
        'CommandBarItem33
        '
        Me.CommandBarItem33.Caption = "&Save"
        Me.CommandBarItem33.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SaveFile
        Me.CommandBarItem33.Enabled = False
        Me.CommandBarItem33.Hint = "Save a report"
        Me.CommandBarItem33.Id = 36
        Me.CommandBarItem33.ImageIndex = 11
        Me.CommandBarItem33.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.CommandBarItem33.Name = "CommandBarItem33"
        '
        'CommandBarItem40
        '
        Me.CommandBarItem40.Caption = "Save &As..."
        Me.CommandBarItem40.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SaveFileAs
        Me.CommandBarItem40.Enabled = False
        Me.CommandBarItem40.Hint = "Save a report with a new name"
        Me.CommandBarItem40.Id = 61
        Me.CommandBarItem40.Name = "CommandBarItem40"
        '
        'CommandBarItem41
        '
        Me.CommandBarItem41.Caption = "Back to the Reports Main Demo"
        Me.CommandBarItem41.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.[Exit]
        Me.CommandBarItem41.Hint = "Close the designer"
        Me.CommandBarItem41.Id = 62
        Me.CommandBarItem41.Name = "CommandBarItem41"
        '
        'BarSubItem2
        '
        Me.BarSubItem2.Caption = "&Edit"
        Me.BarSubItem2.Id = 44
        Me.BarSubItem2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem37, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem38), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem34, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem35), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem36), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem42), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem43, True)})
        Me.BarSubItem2.Name = "BarSubItem2"
        '
        'CommandBarItem37
        '
        Me.CommandBarItem37.Caption = "&Undo"
        Me.CommandBarItem37.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Undo
        Me.CommandBarItem37.Enabled = False
        Me.CommandBarItem37.Hint = "Undo the last operation"
        Me.CommandBarItem37.Id = 40
        Me.CommandBarItem37.ImageIndex = 15
        Me.CommandBarItem37.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z))
        Me.CommandBarItem37.Name = "CommandBarItem37"
        '
        'CommandBarItem38
        '
        Me.CommandBarItem38.Caption = "&Redo"
        Me.CommandBarItem38.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Redo
        Me.CommandBarItem38.Enabled = False
        Me.CommandBarItem38.Hint = "Redo the last operation"
        Me.CommandBarItem38.Id = 41
        Me.CommandBarItem38.ImageIndex = 16
        Me.CommandBarItem38.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y))
        Me.CommandBarItem38.Name = "CommandBarItem38"
        '
        'CommandBarItem34
        '
        Me.CommandBarItem34.Caption = "Cu&t"
        Me.CommandBarItem34.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Cut
        Me.CommandBarItem34.Enabled = False
        Me.CommandBarItem34.Hint = "Delete the control and copy it to the clipboard"
        Me.CommandBarItem34.Id = 37
        Me.CommandBarItem34.ImageIndex = 12
        Me.CommandBarItem34.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X))
        Me.CommandBarItem34.Name = "CommandBarItem34"
        '
        'CommandBarItem35
        '
        Me.CommandBarItem35.Caption = "&Copy"
        Me.CommandBarItem35.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Copy
        Me.CommandBarItem35.Enabled = False
        Me.CommandBarItem35.Hint = "Copy the control to the clipboard"
        Me.CommandBarItem35.Id = 38
        Me.CommandBarItem35.ImageIndex = 13
        Me.CommandBarItem35.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C))
        Me.CommandBarItem35.Name = "CommandBarItem35"
        '
        'CommandBarItem36
        '
        Me.CommandBarItem36.Caption = "&Paste"
        Me.CommandBarItem36.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Paste
        Me.CommandBarItem36.Enabled = False
        Me.CommandBarItem36.Hint = "Add the control from the clipboard"
        Me.CommandBarItem36.Id = 39
        Me.CommandBarItem36.ImageIndex = 14
        Me.CommandBarItem36.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V))
        Me.CommandBarItem36.Name = "CommandBarItem36"
        '
        'CommandBarItem42
        '
        Me.CommandBarItem42.Caption = "&Delete"
        Me.CommandBarItem42.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Delete
        Me.CommandBarItem42.Enabled = False
        Me.CommandBarItem42.Hint = "Delete the control"
        Me.CommandBarItem42.Id = 63
        Me.CommandBarItem42.Name = "CommandBarItem42"
        '
        'CommandBarItem43
        '
        Me.CommandBarItem43.Caption = "Select &All"
        Me.CommandBarItem43.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SelectAll
        Me.CommandBarItem43.Enabled = False
        Me.CommandBarItem43.Hint = "Select all the controls in the document"
        Me.CommandBarItem43.Id = 64
        Me.CommandBarItem43.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.CommandBarItem43.Name = "CommandBarItem43"
        '
        'BarSubItem3
        '
        Me.BarSubItem3.Caption = "&View"
        Me.BarSubItem3.Id = 45
        Me.BarSubItem3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarReportTabButtonsListItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem4, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem5, True)})
        Me.BarSubItem3.Name = "BarSubItem3"
        '
        'BarReportTabButtonsListItem1
        '
        Me.BarReportTabButtonsListItem1.Caption = "Tab Buttons"
        Me.BarReportTabButtonsListItem1.Id = 46
        Me.BarReportTabButtonsListItem1.Name = "BarReportTabButtonsListItem1"
        '
        'BarSubItem4
        '
        Me.BarSubItem4.Caption = "&Toolbars"
        Me.BarSubItem4.Id = 47
        Me.BarSubItem4.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.XrBarToolbarsListItem1)})
        Me.BarSubItem4.Name = "BarSubItem4"
        '
        'XrBarToolbarsListItem1
        '
        Me.XrBarToolbarsListItem1.Caption = "&Toolbars"
        Me.XrBarToolbarsListItem1.Id = 48
        Me.XrBarToolbarsListItem1.Name = "XrBarToolbarsListItem1"
        '
        'BarSubItem5
        '
        Me.BarSubItem5.Caption = "&Windows"
        Me.BarSubItem5.Id = 49
        Me.BarSubItem5.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarDockPanelsListItem1)})
        Me.BarSubItem5.Name = "BarSubItem5"
        '
        'BarDockPanelsListItem1
        '
        Me.BarDockPanelsListItem1.Caption = "&Windows"
        Me.BarDockPanelsListItem1.Id = 50
        Me.BarDockPanelsListItem1.Name = "BarDockPanelsListItem1"
        Me.BarDockPanelsListItem1.ShowCustomizationItem = False
        Me.BarDockPanelsListItem1.ShowDockPanels = True
        Me.BarDockPanelsListItem1.ShowToolbars = False
        '
        'BarSubItem6
        '
        Me.BarSubItem6.Caption = "Fo&rmat"
        Me.BarSubItem6.Id = 51
        Me.BarSubItem6.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandColorBarItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandColorBarItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem7, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem8), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem9, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem11, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem12), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem13, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem14, True)})
        Me.BarSubItem6.Name = "BarSubItem6"
        '
        'CommandColorBarItem1
        '
        Me.CommandColorBarItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.CommandColorBarItem1.Caption = "For&eground Color"
        Me.CommandColorBarItem1.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.ForeColor
        Me.CommandColorBarItem1.Enabled = False
        Me.CommandColorBarItem1.Glyph = CType(resources.GetObject("CommandColorBarItem1.Glyph"), System.Drawing.Image)
        Me.CommandColorBarItem1.Hint = "Set the foreground color of the control"
        Me.CommandColorBarItem1.Id = 5
        Me.CommandColorBarItem1.Name = "CommandColorBarItem1"
        '
        'CommandColorBarItem2
        '
        Me.CommandColorBarItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.CommandColorBarItem2.Caption = "Bac&kground Color"
        Me.CommandColorBarItem2.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.BackColor
        Me.CommandColorBarItem2.Enabled = False
        Me.CommandColorBarItem2.Glyph = CType(resources.GetObject("CommandColorBarItem2.Glyph"), System.Drawing.Image)
        Me.CommandColorBarItem2.Hint = "Set the background color of the control"
        Me.CommandColorBarItem2.Id = 6
        Me.CommandColorBarItem2.Name = "CommandColorBarItem2"
        '
        'BarSubItem7
        '
        Me.BarSubItem7.Caption = "&Font"
        Me.BarSubItem7.Id = 52
        Me.BarSubItem7.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem1, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem3)})
        Me.BarSubItem7.Name = "BarSubItem7"
        '
        'CommandBarItem1
        '
        Me.CommandBarItem1.Caption = "&Bold"
        Me.CommandBarItem1.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.FontBold
        Me.CommandBarItem1.Enabled = False
        Me.CommandBarItem1.Hint = "Make the font bold"
        Me.CommandBarItem1.Id = 2
        Me.CommandBarItem1.ImageIndex = 0
        Me.CommandBarItem1.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B))
        Me.CommandBarItem1.Name = "CommandBarItem1"
        '
        'CommandBarItem2
        '
        Me.CommandBarItem2.Caption = "&Italic"
        Me.CommandBarItem2.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.FontItalic
        Me.CommandBarItem2.Enabled = False
        Me.CommandBarItem2.Hint = "Make the font italic"
        Me.CommandBarItem2.Id = 3
        Me.CommandBarItem2.ImageIndex = 1
        Me.CommandBarItem2.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I))
        Me.CommandBarItem2.Name = "CommandBarItem2"
        '
        'CommandBarItem3
        '
        Me.CommandBarItem3.Caption = "&Underline"
        Me.CommandBarItem3.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.FontUnderline
        Me.CommandBarItem3.Enabled = False
        Me.CommandBarItem3.Hint = "Underline the font"
        Me.CommandBarItem3.Id = 4
        Me.CommandBarItem3.ImageIndex = 2
        Me.CommandBarItem3.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
        Me.CommandBarItem3.Name = "CommandBarItem3"
        '
        'BarSubItem8
        '
        Me.BarSubItem8.Caption = "&Justify"
        Me.BarSubItem8.Id = 53
        Me.BarSubItem8.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem4, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem5), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem6), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem7)})
        Me.BarSubItem8.Name = "BarSubItem8"
        '
        'CommandBarItem4
        '
        Me.CommandBarItem4.Caption = "&Left"
        Me.CommandBarItem4.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyLeft
        Me.CommandBarItem4.Enabled = False
        Me.CommandBarItem4.Hint = "Align the controlís text to the left"
        Me.CommandBarItem4.Id = 7
        Me.CommandBarItem4.ImageIndex = 5
        Me.CommandBarItem4.Name = "CommandBarItem4"
        '
        'CommandBarItem5
        '
        Me.CommandBarItem5.Caption = "&Center"
        Me.CommandBarItem5.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyCenter
        Me.CommandBarItem5.Enabled = False
        Me.CommandBarItem5.Hint = "Align the controlís text to the center"
        Me.CommandBarItem5.Id = 8
        Me.CommandBarItem5.ImageIndex = 6
        Me.CommandBarItem5.Name = "CommandBarItem5"
        '
        'CommandBarItem6
        '
        Me.CommandBarItem6.Caption = "&Right"
        Me.CommandBarItem6.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyRight
        Me.CommandBarItem6.Enabled = False
        Me.CommandBarItem6.Hint = "Align the controlís text to the right"
        Me.CommandBarItem6.Id = 9
        Me.CommandBarItem6.ImageIndex = 7
        Me.CommandBarItem6.Name = "CommandBarItem6"
        '
        'CommandBarItem7
        '
        Me.CommandBarItem7.Caption = "&Justify"
        Me.CommandBarItem7.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyJustify
        Me.CommandBarItem7.Enabled = False
        Me.CommandBarItem7.Hint = "Justify the controlís text"
        Me.CommandBarItem7.Id = 10
        Me.CommandBarItem7.ImageIndex = 8
        Me.CommandBarItem7.Name = "CommandBarItem7"
        '
        'BarSubItem9
        '
        Me.BarSubItem9.Caption = "&Align"
        Me.BarSubItem9.Id = 54
        Me.BarSubItem9.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem9, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem12, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem13), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem14), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem8, True)})
        Me.BarSubItem9.Name = "BarSubItem9"
        '
        'CommandBarItem9
        '
        Me.CommandBarItem9.Caption = "&Lefts"
        Me.CommandBarItem9.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignLeft
        Me.CommandBarItem9.Enabled = False
        Me.CommandBarItem9.Hint = "Left align the selected controls"
        Me.CommandBarItem9.Id = 12
        Me.CommandBarItem9.ImageIndex = 18
        Me.CommandBarItem9.Name = "CommandBarItem9"
        '
        'CommandBarItem10
        '
        Me.CommandBarItem10.Caption = "&Centers"
        Me.CommandBarItem10.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignVerticalCenters
        Me.CommandBarItem10.Enabled = False
        Me.CommandBarItem10.Hint = "Align the centers of the selected controls vertically"
        Me.CommandBarItem10.Id = 13
        Me.CommandBarItem10.ImageIndex = 19
        Me.CommandBarItem10.Name = "CommandBarItem10"
        '
        'CommandBarItem11
        '
        Me.CommandBarItem11.Caption = "&Rights"
        Me.CommandBarItem11.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignRight
        Me.CommandBarItem11.Enabled = False
        Me.CommandBarItem11.Hint = "Right align the selected controls"
        Me.CommandBarItem11.Id = 14
        Me.CommandBarItem11.ImageIndex = 20
        Me.CommandBarItem11.Name = "CommandBarItem11"
        '
        'CommandBarItem12
        '
        Me.CommandBarItem12.Caption = "&Tops"
        Me.CommandBarItem12.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignTop
        Me.CommandBarItem12.Enabled = False
        Me.CommandBarItem12.Hint = "Align the tops of the selected controls"
        Me.CommandBarItem12.Id = 15
        Me.CommandBarItem12.ImageIndex = 21
        Me.CommandBarItem12.Name = "CommandBarItem12"
        '
        'CommandBarItem13
        '
        Me.CommandBarItem13.Caption = "&Middles"
        Me.CommandBarItem13.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignHorizontalCenters
        Me.CommandBarItem13.Enabled = False
        Me.CommandBarItem13.Hint = "Align the centers of the selected controls horizontally"
        Me.CommandBarItem13.Id = 16
        Me.CommandBarItem13.ImageIndex = 22
        Me.CommandBarItem13.Name = "CommandBarItem13"
        '
        'CommandBarItem14
        '
        Me.CommandBarItem14.Caption = "&Bottoms"
        Me.CommandBarItem14.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignBottom
        Me.CommandBarItem14.Enabled = False
        Me.CommandBarItem14.Hint = "Align the bottoms of the selected controls"
        Me.CommandBarItem14.Id = 17
        Me.CommandBarItem14.ImageIndex = 23
        Me.CommandBarItem14.Name = "CommandBarItem14"
        '
        'CommandBarItem8
        '
        Me.CommandBarItem8.Caption = "to &Grid"
        Me.CommandBarItem8.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignToGrid
        Me.CommandBarItem8.Enabled = False
        Me.CommandBarItem8.Hint = "Align the positions of the selected controls to the grid"
        Me.CommandBarItem8.Id = 11
        Me.CommandBarItem8.ImageIndex = 17
        Me.CommandBarItem8.Name = "CommandBarItem8"
        '
        'BarSubItem10
        '
        Me.BarSubItem10.Caption = "&Make Same Size"
        Me.BarSubItem10.Id = 55
        Me.BarSubItem10.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem15, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem16), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem17), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem18)})
        Me.BarSubItem10.Name = "BarSubItem10"
        '
        'CommandBarItem15
        '
        Me.CommandBarItem15.Caption = "&Width"
        Me.CommandBarItem15.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToControlWidth
        Me.CommandBarItem15.Enabled = False
        Me.CommandBarItem15.Hint = "Make the selected controls have the same width"
        Me.CommandBarItem15.Id = 18
        Me.CommandBarItem15.ImageIndex = 24
        Me.CommandBarItem15.Name = "CommandBarItem15"
        '
        'CommandBarItem16
        '
        Me.CommandBarItem16.Caption = "Size to Gri&d"
        Me.CommandBarItem16.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToGrid
        Me.CommandBarItem16.Enabled = False
        Me.CommandBarItem16.Hint = "Size the selected controls to the grid"
        Me.CommandBarItem16.Id = 19
        Me.CommandBarItem16.ImageIndex = 25
        Me.CommandBarItem16.Name = "CommandBarItem16"
        '
        'CommandBarItem17
        '
        Me.CommandBarItem17.Caption = "&Height"
        Me.CommandBarItem17.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToControlHeight
        Me.CommandBarItem17.Enabled = False
        Me.CommandBarItem17.Hint = "Make the selected controls have the same height"
        Me.CommandBarItem17.Id = 20
        Me.CommandBarItem17.ImageIndex = 26
        Me.CommandBarItem17.Name = "CommandBarItem17"
        '
        'CommandBarItem18
        '
        Me.CommandBarItem18.Caption = "&Both"
        Me.CommandBarItem18.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToControl
        Me.CommandBarItem18.Enabled = False
        Me.CommandBarItem18.Hint = "Make the selected controls the same size"
        Me.CommandBarItem18.Id = 21
        Me.CommandBarItem18.ImageIndex = 27
        Me.CommandBarItem18.Name = "CommandBarItem18"
        '
        'BarSubItem11
        '
        Me.BarSubItem11.Caption = "&Horizontal Spacing"
        Me.BarSubItem11.Id = 56
        Me.BarSubItem11.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem19, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem20), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem21), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem22)})
        Me.BarSubItem11.Name = "BarSubItem11"
        '
        'CommandBarItem19
        '
        Me.CommandBarItem19.Caption = "Make &Equal"
        Me.CommandBarItem19.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceMakeEqual
        Me.CommandBarItem19.Enabled = False
        Me.CommandBarItem19.Hint = "Make the spacing between the selected controls equal"
        Me.CommandBarItem19.Id = 22
        Me.CommandBarItem19.ImageIndex = 28
        Me.CommandBarItem19.Name = "CommandBarItem19"
        '
        'CommandBarItem20
        '
        Me.CommandBarItem20.Caption = "&Increase"
        Me.CommandBarItem20.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceIncrease
        Me.CommandBarItem20.Enabled = False
        Me.CommandBarItem20.Hint = "Increase the spacing between the selected controls"
        Me.CommandBarItem20.Id = 23
        Me.CommandBarItem20.ImageIndex = 29
        Me.CommandBarItem20.Name = "CommandBarItem20"
        '
        'CommandBarItem21
        '
        Me.CommandBarItem21.Caption = "&Decrease"
        Me.CommandBarItem21.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceDecrease
        Me.CommandBarItem21.Enabled = False
        Me.CommandBarItem21.Hint = "Decrease the spacing between the selected controls"
        Me.CommandBarItem21.Id = 24
        Me.CommandBarItem21.ImageIndex = 30
        Me.CommandBarItem21.Name = "CommandBarItem21"
        '
        'CommandBarItem22
        '
        Me.CommandBarItem22.Caption = "&Remove"
        Me.CommandBarItem22.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceConcatenate
        Me.CommandBarItem22.Enabled = False
        Me.CommandBarItem22.Hint = "Remove the spacing between the selected controls"
        Me.CommandBarItem22.Id = 25
        Me.CommandBarItem22.ImageIndex = 31
        Me.CommandBarItem22.Name = "CommandBarItem22"
        '
        'BarSubItem12
        '
        Me.BarSubItem12.Caption = "&Vertical Spacing"
        Me.BarSubItem12.Id = 57
        Me.BarSubItem12.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem23, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem24), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem25), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem26)})
        Me.BarSubItem12.Name = "BarSubItem12"
        '
        'CommandBarItem23
        '
        Me.CommandBarItem23.Caption = "Make &Equal"
        Me.CommandBarItem23.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceMakeEqual
        Me.CommandBarItem23.Enabled = False
        Me.CommandBarItem23.Hint = "Make the spacing between the selected controls equal"
        Me.CommandBarItem23.Id = 26
        Me.CommandBarItem23.ImageIndex = 32
        Me.CommandBarItem23.Name = "CommandBarItem23"
        '
        'CommandBarItem24
        '
        Me.CommandBarItem24.Caption = "&Increase"
        Me.CommandBarItem24.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceIncrease
        Me.CommandBarItem24.Enabled = False
        Me.CommandBarItem24.Hint = "Increase the spacing between the selected controls"
        Me.CommandBarItem24.Id = 27
        Me.CommandBarItem24.ImageIndex = 33
        Me.CommandBarItem24.Name = "CommandBarItem24"
        '
        'CommandBarItem25
        '
        Me.CommandBarItem25.Caption = "&Decrease"
        Me.CommandBarItem25.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceDecrease
        Me.CommandBarItem25.Enabled = False
        Me.CommandBarItem25.Hint = "Decrease the spacing between the selected controls"
        Me.CommandBarItem25.Id = 28
        Me.CommandBarItem25.ImageIndex = 34
        Me.CommandBarItem25.Name = "CommandBarItem25"
        '
        'CommandBarItem26
        '
        Me.CommandBarItem26.Caption = "&Remove"
        Me.CommandBarItem26.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceConcatenate
        Me.CommandBarItem26.Enabled = False
        Me.CommandBarItem26.Hint = "Remove the spacing between the selected controls"
        Me.CommandBarItem26.Id = 29
        Me.CommandBarItem26.ImageIndex = 35
        Me.CommandBarItem26.Name = "CommandBarItem26"
        '
        'BarSubItem13
        '
        Me.BarSubItem13.Caption = "&Center in Form"
        Me.BarSubItem13.Id = 58
        Me.BarSubItem13.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem27, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem28)})
        Me.BarSubItem13.Name = "BarSubItem13"
        '
        'CommandBarItem27
        '
        Me.CommandBarItem27.Caption = "&Horizontally"
        Me.CommandBarItem27.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.CenterHorizontally
        Me.CommandBarItem27.Enabled = False
        Me.CommandBarItem27.Hint = "Horizontally center the selected controls within a band"
        Me.CommandBarItem27.Id = 30
        Me.CommandBarItem27.ImageIndex = 36
        Me.CommandBarItem27.Name = "CommandBarItem27"
        '
        'CommandBarItem28
        '
        Me.CommandBarItem28.Caption = "&Vertically"
        Me.CommandBarItem28.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.CenterVertically
        Me.CommandBarItem28.Enabled = False
        Me.CommandBarItem28.Hint = "Vertically center the selected controls within a band"
        Me.CommandBarItem28.Id = 31
        Me.CommandBarItem28.ImageIndex = 37
        Me.CommandBarItem28.Name = "CommandBarItem28"
        '
        'BarSubItem14
        '
        Me.BarSubItem14.Caption = "&Order"
        Me.BarSubItem14.Id = 59
        Me.BarSubItem14.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem29, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem30)})
        Me.BarSubItem14.Name = "BarSubItem14"
        '
        'CommandBarItem29
        '
        Me.CommandBarItem29.Caption = "&Bring to Front"
        Me.CommandBarItem29.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.BringToFront
        Me.CommandBarItem29.Enabled = False
        Me.CommandBarItem29.Hint = "Bring the selected controls to the front"
        Me.CommandBarItem29.Id = 32
        Me.CommandBarItem29.ImageIndex = 38
        Me.CommandBarItem29.Name = "CommandBarItem29"
        '
        'CommandBarItem30
        '
        Me.CommandBarItem30.Caption = "&Send to Back"
        Me.CommandBarItem30.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SendToBack
        Me.CommandBarItem30.Enabled = False
        Me.CommandBarItem30.Hint = "Move the selected controls to the back"
        Me.CommandBarItem30.Id = 33
        Me.CommandBarItem30.ImageIndex = 39
        Me.CommandBarItem30.Name = "CommandBarItem30"
        '
        'bsiLookAndFeel
        '
        Me.bsiLookAndFeel.Caption = "&Look and Feel"
        Me.bsiLookAndFeel.Id = 65
        Me.bsiLookAndFeel.Name = "bsiLookAndFeel"
        '
        'DesignBar2
        '
        Me.DesignBar2.BarName = "ToolBar"
        Me.DesignBar2.DockCol = 0
        Me.DesignBar2.DockRow = 1
        Me.DesignBar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.DesignBar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem31), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem32), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem33), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem34, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem35), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem36), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem37, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem38)})
        Me.DesignBar2.Text = "Main Toolbar"
        '
        'DesignBar3
        '
        Me.DesignBar3.BarName = "FormattingToolBar"
        Me.DesignBar3.DockCol = 1
        Me.DesignBar3.DockRow = 1
        Me.DesignBar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.DesignBar3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarEditItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandColorBarItem1, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandColorBarItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem4, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem5), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem6), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem7)})
        Me.DesignBar3.Text = "Formatting Toolbar"
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "Font Name"
        Me.BarEditItem1.Edit = Me.ricbFontName
        Me.BarEditItem1.Hint = "Font Name"
        Me.BarEditItem1.Id = 0
        Me.BarEditItem1.Name = "BarEditItem1"
        Me.BarEditItem1.Width = 120
        '
        'ricbFontName
        '
        Me.ricbFontName.AutoHeight = False
        Me.ricbFontName.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ricbFontName.DropDownRows = 12
        Me.ricbFontName.Name = "ricbFontName"
        '
        'BarEditItem2
        '
        Me.BarEditItem2.Caption = "Font Size"
        Me.BarEditItem2.Edit = Me.ricbFontSize
        Me.BarEditItem2.Hint = "Font Size"
        Me.BarEditItem2.Id = 1
        Me.BarEditItem2.Name = "BarEditItem2"
        '
        'ricbFontSize
        '
        Me.ricbFontSize.AutoHeight = False
        Me.ricbFontSize.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ricbFontSize.Items.AddRange(New Object() {CType(8, Byte), CType(9, Byte), CType(10, Byte), CType(11, Byte), CType(12, Byte), CType(14, Byte), CType(16, Byte), CType(18, Byte), CType(20, Byte), CType(22, Byte), CType(24, Byte), CType(26, Byte), CType(28, Byte), CType(36, Byte), CType(48, Byte), CType(72, Byte)})
        Me.ricbFontSize.Name = "ricbFontSize"
        '
        'DesignBar4
        '
        Me.DesignBar4.BarName = "LayoutToolBar"
        Me.DesignBar4.DockCol = 0
        Me.DesignBar4.DockRow = 2
        Me.DesignBar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.DesignBar4.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem8), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem9, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem12, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem13), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem14), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem15, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem16), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem17), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem18), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem19, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem20), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem21), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem22), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem23, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem24), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem25), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem26), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem27, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem28), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem29, True), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem30)})
        Me.DesignBar4.Text = "Layout Toolbar"
        '
        'DesignBar5
        '
        Me.DesignBar5.BarName = "StatusBar"
        Me.DesignBar5.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.DesignBar5.DockCol = 0
        Me.DesignBar5.DockRow = 0
        Me.DesignBar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.DesignBar5.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1)})
        Me.DesignBar5.OptionsBar.AllowQuickCustomization = False
        Me.DesignBar5.OptionsBar.DrawDragBorder = False
        Me.DesignBar5.OptionsBar.UseWholeRow = True
        Me.DesignBar5.Text = "Status Bar"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring
        Me.BarStaticItem1.Id = 42
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        Me.BarStaticItem1.Width = 32
        '
        'DesignBar6
        '
        Me.DesignBar6.BarName = "Zoom Bar"
        Me.DesignBar6.DockCol = 1
        Me.DesignBar6.DockRow = 2
        Me.DesignBar6.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.DesignBar6.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem44), New DevExpress.XtraBars.LinkPersistInfo(Me.XrZoomBarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.CommandBarItem45)})
        Me.DesignBar6.Text = "Zoom Bar"
        '
        'CommandBarItem44
        '
        Me.CommandBarItem44.Caption = "Zoom Out"
        Me.CommandBarItem44.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.ZoomOut
        Me.CommandBarItem44.Enabled = False
        Me.CommandBarItem44.Hint = "Zoom out the design surface"
        Me.CommandBarItem44.Id = 66
        Me.CommandBarItem44.ImageIndex = 40
        Me.CommandBarItem44.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Subtract))
        Me.CommandBarItem44.Name = "CommandBarItem44"
        '
        'XrZoomBarEditItem1
        '
        Me.XrZoomBarEditItem1.Caption = "Zoom"
        Me.XrZoomBarEditItem1.Edit = Me.DesignRepositoryItemComboBox1
        Me.XrZoomBarEditItem1.Enabled = False
        Me.XrZoomBarEditItem1.Hint = "Select or input the zoom factor"
        Me.XrZoomBarEditItem1.Id = 67
        Me.XrZoomBarEditItem1.Name = "XrZoomBarEditItem1"
        Me.XrZoomBarEditItem1.Width = 70
        '
        'DesignRepositoryItemComboBox1
        '
        Me.DesignRepositoryItemComboBox1.AutoComplete = False
        Me.DesignRepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DesignRepositoryItemComboBox1.Name = "DesignRepositoryItemComboBox1"
        '
        'CommandBarItem45
        '
        Me.CommandBarItem45.Caption = "Zoom In"
        Me.CommandBarItem45.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.ZoomIn
        Me.CommandBarItem45.Enabled = False
        Me.CommandBarItem45.Hint = "Zoom in the design surface"
        Me.CommandBarItem45.Id = 68
        Me.CommandBarItem45.ImageIndex = 41
        Me.CommandBarItem45.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Add))
        Me.CommandBarItem45.Name = "CommandBarItem45"
        '
        'XrDesignDockManager1
        '
        Me.XrDesignDockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerLeft})
        Me.XrDesignDockManager1.Form = Me
        Me.XrDesignDockManager1.ImageStream = CType(resources.GetObject("XrDesignDockManager1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.XrDesignDockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.panelContainer1, Me.GroupAndSortDockPanel1})
        Me.XrDesignDockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar"})
        Me.XrDesignDockManager1.XRDesignPanel = Me.xrDesignPanel
        '
        'hideContainerLeft
        '
        Me.hideContainerLeft.Controls.Add(Me.ToolBoxDockPanel1)
        Me.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.hideContainerLeft.Location = New System.Drawing.Point(0, 78)
        Me.hideContainerLeft.Name = "hideContainerLeft"
        Me.hideContainerLeft.Size = New System.Drawing.Size(22, 533)
        '
        'ToolBoxDockPanel1
        '
        Me.ToolBoxDockPanel1.Controls.Add(Me.ToolBoxDockPanel1_Container)
        Me.ToolBoxDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.ToolBoxDockPanel1.ID = New System.Guid("161a5a1a-d9b9-4f06-9ac4-d0c3e507c54f")
        Me.ToolBoxDockPanel1.ImageIndex = 4
        Me.ToolBoxDockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBoxDockPanel1.Name = "ToolBoxDockPanel1"
        Me.ToolBoxDockPanel1.OriginalSize = New System.Drawing.Size(200, 200)
        Me.ToolBoxDockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.ToolBoxDockPanel1.SavedIndex = 1
        Me.ToolBoxDockPanel1.Size = New System.Drawing.Size(200, 535)
        Me.ToolBoxDockPanel1.Text = "Tool Box"
        Me.ToolBoxDockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        Me.ToolBoxDockPanel1.XRDesignPanel = Me.xrDesignPanel
        '
        'ToolBoxDockPanel1_Container
        '
        Me.ToolBoxDockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.ToolBoxDockPanel1_Container.Name = "ToolBoxDockPanel1_Container"
        Me.ToolBoxDockPanel1_Container.Size = New System.Drawing.Size(194, 507)
        Me.ToolBoxDockPanel1_Container.TabIndex = 0
        '
        'panelContainer1
        '
        Me.panelContainer1.Controls.Add(Me.panelContainer2)
        Me.panelContainer1.Controls.Add(Me.PropertyGridDockPanel1)
        Me.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right
        Me.panelContainer1.ID = New System.Guid("78d8c43d-0d28-4407-a385-e42eb0fafd90")
        Me.panelContainer1.Location = New System.Drawing.Point(587, 78)
        Me.panelContainer1.Name = "panelContainer1"
        Me.panelContainer1.OriginalSize = New System.Drawing.Size(200, 200)
        Me.panelContainer1.Size = New System.Drawing.Size(200, 333)
        Me.panelContainer1.Text = "panelContainer1"
        '
        'panelContainer2
        '
        Me.panelContainer2.ActiveChild = Me.ReportExplorerDockPanel1
        Me.panelContainer2.Controls.Add(Me.ReportExplorerDockPanel1)
        Me.panelContainer2.Controls.Add(Me.FieldListDockPanel1)
        Me.panelContainer2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill
        Me.panelContainer2.ID = New System.Guid("42adb96b-9046-40af-a454-1cf5ddeb4c28")
        Me.panelContainer2.ImageIndex = 3
        Me.panelContainer2.Location = New System.Drawing.Point(0, 0)
        Me.panelContainer2.Name = "panelContainer2"
        Me.panelContainer2.OriginalSize = New System.Drawing.Size(200, 683)
        Me.panelContainer2.Size = New System.Drawing.Size(200, 1093)
        Me.panelContainer2.Tabbed = True
        Me.panelContainer2.Text = "panelContainer2"
        '
        'ReportExplorerDockPanel1
        '
        Me.ReportExplorerDockPanel1.Controls.Add(Me.ReportExplorerDockPanel1_Container)
        Me.ReportExplorerDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill
        Me.ReportExplorerDockPanel1.ID = New System.Guid("fb3ec6cc-3b9b-4b9c-91cf-cff78c1edbf1")
        Me.ReportExplorerDockPanel1.ImageIndex = 3
        Me.ReportExplorerDockPanel1.Location = New System.Drawing.Point(3, 25)
        Me.ReportExplorerDockPanel1.Name = "ReportExplorerDockPanel1"
        Me.ReportExplorerDockPanel1.OriginalSize = New System.Drawing.Size(194, 632)
        Me.ReportExplorerDockPanel1.Size = New System.Drawing.Size(194, 1042)
        Me.ReportExplorerDockPanel1.Text = "Report Explorer"
        Me.ReportExplorerDockPanel1.XRDesignPanel = Me.xrDesignPanel
        '
        'ReportExplorerDockPanel1_Container
        '
        Me.ReportExplorerDockPanel1_Container.Location = New System.Drawing.Point(0, 0)
        Me.ReportExplorerDockPanel1_Container.Name = "ReportExplorerDockPanel1_Container"
        Me.ReportExplorerDockPanel1_Container.Size = New System.Drawing.Size(194, 1042)
        Me.ReportExplorerDockPanel1_Container.TabIndex = 0
        '
        'FieldListDockPanel1
        '
        Me.FieldListDockPanel1.Controls.Add(Me.FieldListDockPanel1_Container)
        Me.FieldListDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill
        Me.FieldListDockPanel1.ID = New System.Guid("faf69838-a93f-4114-83e8-d0d09cc5ce95")
        Me.FieldListDockPanel1.ImageIndex = 0
        Me.FieldListDockPanel1.Location = New System.Drawing.Point(3, 25)
        Me.FieldListDockPanel1.Name = "FieldListDockPanel1"
        Me.FieldListDockPanel1.OriginalSize = New System.Drawing.Size(194, 632)
        Me.FieldListDockPanel1.Size = New System.Drawing.Size(194, 1042)
        Me.FieldListDockPanel1.Text = "Field List"
        Me.FieldListDockPanel1.XRDesignPanel = Me.xrDesignPanel
        '
        'FieldListDockPanel1_Container
        '
        Me.FieldListDockPanel1_Container.Location = New System.Drawing.Point(0, 0)
        Me.FieldListDockPanel1_Container.Name = "FieldListDockPanel1_Container"
        Me.FieldListDockPanel1_Container.Size = New System.Drawing.Size(194, 1042)
        Me.FieldListDockPanel1_Container.TabIndex = 0
        '
        'PropertyGridDockPanel1
        '
        Me.PropertyGridDockPanel1.Controls.Add(Me.PropertyGridDockPanel1_Container)
        Me.PropertyGridDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill
        Me.PropertyGridDockPanel1.ID = New System.Guid("b38d12c3-cd06-4dec-b93d-63a0088e495a")
        Me.PropertyGridDockPanel1.ImageIndex = 2
        Me.PropertyGridDockPanel1.Location = New System.Drawing.Point(0, 1093)
        Me.PropertyGridDockPanel1.Name = "PropertyGridDockPanel1"
        Me.PropertyGridDockPanel1.OriginalSize = New System.Drawing.Size(200, 0)
        Me.PropertyGridDockPanel1.Size = New System.Drawing.Size(200, 0)
        Me.PropertyGridDockPanel1.Text = "Property Grid"
        Me.PropertyGridDockPanel1.XRDesignPanel = Me.xrDesignPanel
        '
        'PropertyGridDockPanel1_Container
        '
        Me.PropertyGridDockPanel1_Container.Location = New System.Drawing.Point(3, -560)
        Me.PropertyGridDockPanel1_Container.Name = "PropertyGridDockPanel1_Container"
        Me.PropertyGridDockPanel1_Container.Size = New System.Drawing.Size(194, 0)
        Me.PropertyGridDockPanel1_Container.TabIndex = 0
        '
        'GroupAndSortDockPanel1
        '
        Me.GroupAndSortDockPanel1.Controls.Add(Me.GroupAndSortDockPanel1_Container)
        Me.GroupAndSortDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.GroupAndSortDockPanel1.ID = New System.Guid("4bab159e-c495-4d67-87dc-f4e895da443e")
        Me.GroupAndSortDockPanel1.ImageIndex = 1
        Me.GroupAndSortDockPanel1.Location = New System.Drawing.Point(22, 411)
        Me.GroupAndSortDockPanel1.Name = "GroupAndSortDockPanel1"
        Me.GroupAndSortDockPanel1.OriginalSize = New System.Drawing.Size(200, 200)
        Me.GroupAndSortDockPanel1.Size = New System.Drawing.Size(765, 200)
        Me.GroupAndSortDockPanel1.Text = "Group and Sort"
        Me.GroupAndSortDockPanel1.XRDesignPanel = Me.xrDesignPanel
        '
        'GroupAndSortDockPanel1_Container
        '
        Me.GroupAndSortDockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.GroupAndSortDockPanel1_Container.Name = "GroupAndSortDockPanel1_Container"
        Me.GroupAndSortDockPanel1_Container.Size = New System.Drawing.Size(559, 172)
        Me.GroupAndSortDockPanel1_Container.TabIndex = 0
        '
        'CustomDesignForm
        '
        Me.ClientSize = New System.Drawing.Size(787, 636)
        Me.Controls.Add(Me.panelContainer1)
        Me.Controls.Add(Me.GroupAndSortDockPanel1)
        Me.Controls.Add(Me.hideContainerLeft)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.MinimumSize = New System.Drawing.Size(358, 209)
        Me.Name = "CustomDesignForm"
        Me.Text = "CustomDesignForm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.hideContainerLeft, 0)
        Me.Controls.SetChildIndex(Me.GroupAndSortDockPanel1, 0)
        Me.Controls.SetChildIndex(Me.panelContainer1, 0)
        Me.Controls.SetChildIndex(Me.xrDesignPanel, 0)
        CType(Me.xrDesignPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrDesignBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ricbFontName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ricbFontSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DesignRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrDesignDockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerLeft.ResumeLayout(False)
        Me.ToolBoxDockPanel1.ResumeLayout(False)
        Me.panelContainer1.ResumeLayout(False)
        Me.panelContainer2.ResumeLayout(False)
        Me.ReportExplorerDockPanel1.ResumeLayout(False)
        Me.FieldListDockPanel1.ResumeLayout(False)
        Me.PropertyGridDockPanel1.ResumeLayout(False)
        Me.GroupAndSortDockPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Protected Overrides Sub SaveLayout()
    End Sub 'SaveLayout

    Protected Overrides Sub RestoreLayout()
    End Sub 'RestoreLayout
End Class
