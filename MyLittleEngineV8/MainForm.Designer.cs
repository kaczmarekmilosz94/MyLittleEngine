namespace MyLittleEngineV8
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyœæ wszystkie u¿ywane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, je¿eli zarz¹dzane zasoby powinny zostaæ zlikwidowane; Fa³sz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obs³ugi projektanta — nie nale¿y modyfikowaæ
        /// jej zawartoœci w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.scenePage = new System.Windows.Forms.TabPage();
            this.gamePage = new System.Windows.Forms.TabPage();
            this.treeViewGOs = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createGameObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Inspector = new System.Windows.Forms.GroupBox();
            this.componentsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddComponent = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.objectName = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView = new System.Windows.Forms.ListView();
            this.contextMenuAssets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBackFolder = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.contextMenuComps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meshRendererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxColliderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rigidbodyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelFPS = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileUserScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.componentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.editorView = new System.Windows.Forms.MyDisplay();
            this.gameView = new System.Windows.Forms.MyDisplay();
            this.labelNoCamera = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scriptNameText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.scenePage.SuspendLayout();
            this.gamePage.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.Inspector.SuspendLayout();
            this.componentsLayout.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuAssets.SuspendLayout();
            this.contextMenuComps.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.editorView.SuspendLayout();
            this.gameView.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 17;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.scenePage);
            this.tabControl1.Controls.Add(this.gamePage);
            this.tabControl1.Location = new System.Drawing.Point(224, 44);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(978, 575);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TabControl1_KeyDown);
            this.tabControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TabControl1_KeyUp);
            // 
            // scenePage
            // 
            this.scenePage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.scenePage.Controls.Add(this.editorView);
            this.scenePage.Location = new System.Drawing.Point(4, 22);
            this.scenePage.Name = "scenePage";
            this.scenePage.Padding = new System.Windows.Forms.Padding(3);
            this.scenePage.Size = new System.Drawing.Size(970, 549);
            this.scenePage.TabIndex = 0;
            this.scenePage.Text = "Scene";
            // 
            // gamePage
            // 
            this.gamePage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gamePage.Controls.Add(this.gameView);
            this.gamePage.Location = new System.Drawing.Point(4, 22);
            this.gamePage.Margin = new System.Windows.Forms.Padding(0);
            this.gamePage.Name = "gamePage";
            this.gamePage.Padding = new System.Windows.Forms.Padding(3);
            this.gamePage.Size = new System.Drawing.Size(970, 549);
            this.gamePage.TabIndex = 2;
            this.gamePage.Text = "Game";
            // 
            // treeViewGOs
            // 
            this.treeViewGOs.AllowDrop = true;
            this.treeViewGOs.BackColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewGOs.ContextMenuStrip = this.contextMenuStrip1;
            this.treeViewGOs.Location = new System.Drawing.Point(6, 22);
            this.treeViewGOs.Name = "treeViewGOs";
            this.treeViewGOs.Size = new System.Drawing.Size(179, 555);
            this.treeViewGOs.TabIndex = 4;
            this.treeViewGOs.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.GameObject_ItemDrag);
            this.treeViewGOs.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.TreeViewGOs_NodeMouseHover);
            this.treeViewGOs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewGOs_AfterSelect);
            this.treeViewGOs.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeViewGOs_DragDrop);
            this.treeViewGOs.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeViewGOs_DragEnter);
            this.treeViewGOs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeViewGOs_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createGameObjectToolStripMenuItem,
            this.createCameraToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 48);
            // 
            // createGameObjectToolStripMenuItem
            // 
            this.createGameObjectToolStripMenuItem.Name = "createGameObjectToolStripMenuItem";
            this.createGameObjectToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.createGameObjectToolStripMenuItem.Text = "Create GameObject";
            this.createGameObjectToolStripMenuItem.Click += new System.EventHandler(this.CreateGameObjectToolStripMenuItem_Click);
            // 
            // createCameraToolStripMenuItem
            // 
            this.createCameraToolStripMenuItem.Name = "createCameraToolStripMenuItem";
            this.createCameraToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.createCameraToolStripMenuItem.Text = "Create Camera";
            this.createCameraToolStripMenuItem.Click += new System.EventHandler(this.CreateCameraToolStripMenuItem_Click);
            // 
            // Inspector
            // 
            this.Inspector.BackColor = System.Drawing.SystemColors.Control;
            this.Inspector.Controls.Add(this.componentsLayout);
            this.Inspector.Controls.Add(this.label10);
            this.Inspector.Controls.Add(this.objectName);
            this.Inspector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Inspector.Location = new System.Drawing.Point(1208, 27);
            this.Inspector.Name = "Inspector";
            this.Inspector.Size = new System.Drawing.Size(462, 831);
            this.Inspector.TabIndex = 5;
            this.Inspector.TabStop = false;
            this.Inspector.Text = "Inspector";
            // 
            // componentsLayout
            // 
            this.componentsLayout.AllowDrop = true;
            this.componentsLayout.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.componentsLayout.AutoScroll = true;
            this.componentsLayout.BackColor = System.Drawing.SystemColors.ControlDark;
            this.componentsLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.componentsLayout.Controls.Add(this.btnAddComponent);
            this.componentsLayout.Location = new System.Drawing.Point(15, 60);
            this.componentsLayout.Name = "componentsLayout";
            this.componentsLayout.Size = new System.Drawing.Size(410, 755);
            this.componentsLayout.TabIndex = 16;
            this.componentsLayout.Tag = "";
            this.componentsLayout.DragDrop += new System.Windows.Forms.DragEventHandler(this.ComponentsLayout_DragDrop);
            this.componentsLayout.DragEnter += new System.Windows.Forms.DragEventHandler(this.ComponentsLayout_DragEnter);
            // 
            // btnAddComponent
            // 
            this.btnAddComponent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddComponent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddComponent.Location = new System.Drawing.Point(3, 3);
            this.btnAddComponent.Name = "btnAddComponent";
            this.btnAddComponent.Size = new System.Drawing.Size(396, 28);
            this.btnAddComponent.TabIndex = 14;
            this.btnAddComponent.Text = "Add Component";
            this.btnAddComponent.UseVisualStyleBackColor = true;
            this.btnAddComponent.Click += new System.EventHandler(this.BtnAddComponent_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(44, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 20);
            this.label10.TabIndex = 9;
            this.label10.Text = "Name";
            // 
            // objectName
            // 
            this.objectName.Location = new System.Drawing.Point(115, 24);
            this.objectName.Name = "objectName";
            this.objectName.Size = new System.Drawing.Size(260, 20);
            this.objectName.TabIndex = 0;
            this.objectName.TextChanged += new System.EventHandler(this.ObjectName_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add(this.buttonExport);
            this.groupBox4.Controls.Add(this.treeViewGOs);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Location = new System.Drawing.Point(12, 34);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(191, 583);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Hierarchy";
            // 
            // buttonExport
            // 
            this.buttonExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonExport.Location = new System.Drawing.Point(40, 503);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(90, 23);
            this.buttonExport.TabIndex = 7;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "open_folder.ico");
            // 
            // listView
            // 
            this.listView.AllowDrop = true;
            this.listView.BackColor = System.Drawing.SystemColors.GrayText;
            this.listView.ContextMenuStrip = this.contextMenuAssets;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageList1;
            this.listView.Location = new System.Drawing.Point(224, 634);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(978, 215);
            this.listView.TabIndex = 9;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
            this.listView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop);
            this.listView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListView_DragEnter);
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDoubleClick);
            // 
            // contextMenuAssets
            // 
            this.contextMenuAssets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuAssets.Name = "contextMenuStrip1";
            this.contextMenuAssets.Size = new System.Drawing.Size(185, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem1.Text = "Create new C# Script";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // btnBackFolder
            // 
            this.btnBackFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBackFolder.Location = new System.Drawing.Point(143, 634);
            this.btnBackFolder.Name = "btnBackFolder";
            this.btnBackFolder.Size = new System.Drawing.Size(75, 23);
            this.btnBackFolder.TabIndex = 10;
            this.btnBackFolder.Text = "Back";
            this.btnBackFolder.UseVisualStyleBackColor = true;
            this.btnBackFolder.Click += new System.EventHandler(this.BtnBackFolder_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnStartStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStartStop.Location = new System.Drawing.Point(643, 34);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(134, 26);
            this.btnStartStop.TabIndex = 11;
            this.btnStartStop.Text = "Play";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.BtnStartStop_Click);
            // 
            // contextMenuComps
            // 
            this.contextMenuComps.AutoSize = false;
            this.contextMenuComps.BackColor = System.Drawing.SystemColors.ControlDark;
            this.contextMenuComps.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.contextMenuComps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightToolStripMenuItem,
            this.meshRendererToolStripMenuItem,
            this.audioToolStripMenuItem,
            this.newScriptToolStripMenuItem,
            this.cameraToolStripMenuItem,
            this.boxColliderToolStripMenuItem,
            this.rigidbodyToolStripMenuItem});
            this.contextMenuComps.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.contextMenuComps.Name = "contextMenuComps";
            this.contextMenuComps.ShowCheckMargin = true;
            this.contextMenuComps.Size = new System.Drawing.Size(220, 170);
            this.contextMenuComps.Text = "fdgdfg";
            this.contextMenuComps.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ScriptToolStripMenuItem_Click);
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.lightToolStripMenuItem.Text = "Light";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.LightToolStripMenuItem_Click);
            // 
            // meshRendererToolStripMenuItem
            // 
            this.meshRendererToolStripMenuItem.Name = "meshRendererToolStripMenuItem";
            this.meshRendererToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.meshRendererToolStripMenuItem.Text = "Mesh renderer";
            this.meshRendererToolStripMenuItem.Click += new System.EventHandler(this.MeshRendererToolStripMenuItem_Click);
            // 
            // audioToolStripMenuItem
            // 
            this.audioToolStripMenuItem.Name = "audioToolStripMenuItem";
            this.audioToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.audioToolStripMenuItem.Text = "Audio";
            // 
            // newScriptToolStripMenuItem
            // 
            this.newScriptToolStripMenuItem.Name = "newScriptToolStripMenuItem";
            this.newScriptToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.newScriptToolStripMenuItem.Text = "New script";
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.cameraToolStripMenuItem.Text = "Camera";
            this.cameraToolStripMenuItem.Click += new System.EventHandler(this.CameraToolStripMenuItem_Click);
            // 
            // boxColliderToolStripMenuItem
            // 
            this.boxColliderToolStripMenuItem.Name = "boxColliderToolStripMenuItem";
            this.boxColliderToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.boxColliderToolStripMenuItem.Text = "Box Collider";
            this.boxColliderToolStripMenuItem.Click += new System.EventHandler(this.BoxColliderToolStripMenuItem_Click);
            // 
            // rigidbodyToolStripMenuItem
            // 
            this.rigidbodyToolStripMenuItem.Name = "rigidbodyToolStripMenuItem";
            this.rigidbodyToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.rigidbodyToolStripMenuItem.Text = "Rigidbody";
            this.rigidbodyToolStripMenuItem.Click += new System.EventHandler(this.RigidbodyToolStripMenuItem_Click);
            // 
            // labelFPS
            // 
            this.labelFPS.AutoSize = true;
            this.labelFPS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelFPS.Location = new System.Drawing.Point(1030, 44);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(0, 13);
            this.labelFPS.TabIndex = 13;
            this.labelFPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.assetsToolStripMenuItem,
            this.gameObjectToolStripMenuItem,
            this.componentToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1652, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSceneToolStripMenuItem,
            this.openSceneToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newSceneToolStripMenuItem
            // 
            this.newSceneToolStripMenuItem.Name = "newSceneToolStripMenuItem";
            this.newSceneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newSceneToolStripMenuItem.Text = "New Scene";
            this.newSceneToolStripMenuItem.Click += new System.EventHandler(this.NewSceneToolStripMenuItem_Click);
            // 
            // openSceneToolStripMenuItem
            // 
            this.openSceneToolStripMenuItem.Name = "openSceneToolStripMenuItem";
            this.openSceneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openSceneToolStripMenuItem.Text = "Open Scene";
            this.openSceneToolStripMenuItem.Click += new System.EventHandler(this.OpenSceneToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // assetsToolStripMenuItem
            // 
            this.assetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileUserScriptsToolStripMenuItem});
            this.assetsToolStripMenuItem.Name = "assetsToolStripMenuItem";
            this.assetsToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.assetsToolStripMenuItem.Text = "Assets";
            // 
            // compileUserScriptsToolStripMenuItem
            // 
            this.compileUserScriptsToolStripMenuItem.Name = "compileUserScriptsToolStripMenuItem";
            this.compileUserScriptsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.compileUserScriptsToolStripMenuItem.Text = "Compile user scripts";
            this.compileUserScriptsToolStripMenuItem.Click += new System.EventHandler(this.CompileUserScriptsToolStripMenuItem_Click);
            // 
            // gameObjectToolStripMenuItem
            // 
            this.gameObjectToolStripMenuItem.Name = "gameObjectToolStripMenuItem";
            this.gameObjectToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.gameObjectToolStripMenuItem.Text = "GameObject";
            // 
            // componentToolStripMenuItem
            // 
            this.componentToolStripMenuItem.Name = "componentToolStripMenuItem";
            this.componentToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.componentToolStripMenuItem.Text = "Component";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "scene";
            // 
            // editorView
            // 
            this.editorView.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.editorView.Controls.Add(this.panel1);
            this.editorView.Location = new System.Drawing.Point(4, 6);
            this.editorView.Name = "editorView";
            this.editorView.Size = new System.Drawing.Size(960, 540);
            this.editorView.TabIndex = 1;
            this.editorView.Paint += new System.Windows.Forms.PaintEventHandler(this.EditorView_Paint);
            this.editorView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TabControl1_MouseDown);
            this.editorView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseMove);
            this.editorView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TabControl1_MouseUp);
            // 
            // gameView
            // 
            this.gameView.BackColor = System.Drawing.SystemColors.GrayText;
            this.gameView.Controls.Add(this.labelNoCamera);
            this.gameView.Location = new System.Drawing.Point(4, 6);
            this.gameView.Name = "gameView";
            this.gameView.Size = new System.Drawing.Size(960, 540);
            this.gameView.TabIndex = 3;
            this.gameView.Paint += new System.Windows.Forms.PaintEventHandler(this.GameView_Paint);
            // 
            // labelNoCamera
            // 
            this.labelNoCamera.AutoSize = true;
            this.labelNoCamera.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold);
            this.labelNoCamera.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelNoCamera.Location = new System.Drawing.Point(323, 250);
            this.labelNoCamera.Name = "labelNoCamera";
            this.labelNoCamera.Size = new System.Drawing.Size(327, 34);
            this.labelNoCamera.TabIndex = 0;
            this.labelNoCamera.Text = "Main camera is not set";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.scriptNameText);
            this.panel1.Location = new System.Drawing.Point(341, 453);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 69);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // scriptNameText
            // 
            this.scriptNameText.Location = new System.Drawing.Point(16, 15);
            this.scriptNameText.Name = "scriptNameText";
            this.scriptNameText.Size = new System.Drawing.Size(240, 20);
            this.scriptNameText.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(181, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1652, 861);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.labelFPS);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnBackFolder);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.Inspector);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyLittleEngine";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.scenePage.ResumeLayout(false);
            this.gamePage.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.Inspector.ResumeLayout(false);
            this.Inspector.PerformLayout();
            this.componentsLayout.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.contextMenuAssets.ResumeLayout(false);
            this.contextMenuComps.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.editorView.ResumeLayout(false);
            this.gameView.ResumeLayout(false);
            this.gameView.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage scenePage;
        private System.Windows.Forms.MyDisplay editorView;
        private System.Windows.Forms.TabPage gamePage;
        private System.Windows.Forms.MyDisplay gameView;
        internal System.Windows.Forms.TreeView treeViewGOs;
        private System.Windows.Forms.GroupBox Inspector;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox objectName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createGameObjectToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btnBackFolder;
        private System.Windows.Forms.Button btnAddComponent;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.FlowLayoutPanel componentsLayout;
        private System.Windows.Forms.ContextMenuStrip contextMenuComps;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meshRendererToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newScriptToolStripMenuItem;
        private System.Windows.Forms.Label labelFPS;
        private System.Windows.Forms.Label labelNoCamera;
        private System.Windows.Forms.ToolStripMenuItem createCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem componentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuAssets;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem compileUserScriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boxColliderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rigidbodyToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox scriptNameText;
    }
}

