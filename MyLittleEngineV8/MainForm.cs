using System;
using System.Threading;
using System.IO;
using Microsoft.Build.Evaluation;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using Microsoft.Build.Execution;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using MyLittleRendererV8;

namespace MyLittleEngineV8
{
    public partial class MainForm : Form
    {
        bool playMode = false; // set 'true' to start playing , 'false' for edit mode

        float fps = 0;
        int fpsDelay = 0;

        #region Movement 


        int mouseX = Cursor.Position.X;
        int mouseY = Cursor.Position.Y;
        int dx = 0;
        int dy = 0;

        bool letRotate = false; // Allows to rotate only if holding Right Mouse Button
        #endregion

        GameObject selectedGO;
        GameObject draggedGO;
        GroupBox transformBox;


        private Camera editorCamera = new Camera();

        readonly Dictionary<string, Script> userScripts = new Dictionary<string, Script>();

        readonly List<string> fileNames = new List<string>();

        internal string currentSceneName;
        internal string projectName;
        internal string originalPath;
        internal string currentPath;
        internal string previousPath;

        public MainForm()
        {
            InitializeComponent();
        }


        //OnStart fucntion
        private void Form1_Load(object sender, EventArgs e)
        {
            Input.PrepareKeys();
            Scene scene = new Scene();
            Scene.currentScene = scene;

            editorCamera = new Camera();

            GetFilesList(currentPath);

            if (File.Exists(originalPath + "\\temp\\temp"))
            {
                foreach (string path in Directory.GetFiles(@"..\Debug\temp"))
                {
                    File.Delete(path);
                }
                LoadScripts(true);

            }
            else
            {
                LoadScripts(false);
            }
        }

        // Start function
        private void StartFunctions()
        {
            foreach (GameObject go in Scene.currentScene.gameObjects)
            {
                Script[] scripts = go.GetComponents<Script>();
                RigidbodyComponent rigidbodyComponent = go.GetComponent<RigidbodyComponent>();

                if (scripts != null)
                {
                    foreach (Script s in scripts)
                    {
                        try
                        {
                            s.Update();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }
                }
            }
        }
        //Update function
        private void Timer1_Tick(object sender, EventArgs e)
        {
            editorView.Invalidate();
            gameView.Invalidate();

            if (playMode)
            {

                foreach (GameObject go in Scene.currentScene.gameObjects)
                {
                    Script[] scripts = go.GetComponents<Script>();
                    RigidbodyComponent rigidbodyComponent = go.GetComponent<RigidbodyComponent>();

                    if (scripts != null)
                    {
                        foreach (Script s in scripts)
                        {
                            try
                            {
                                s.Update();
                            }
                            catch(Exception ex)
                            {
                                Debug.WriteLine(ex);
                            }
                        }
                    }
                    if (rigidbodyComponent != null)
                    {
                        go.GetComponent<Transform>().Translate(rigidbodyComponent.Rigidbody.GetVelocity(), false);
                    }
                }

                if (selectedGO != null)
                {
                    transformBox.Controls[2].Text = selectedGO.GetComponent<Transform>().position.x.ToString();
                    transformBox.Controls[4].Text = selectedGO.GetComponent<Transform>().position.y.ToString();
                    transformBox.Controls[6].Text = selectedGO.GetComponent<Transform>().position.z.ToString();
                    transformBox.Controls[9].Text = selectedGO.GetComponent<Transform>().rotation.x.ToString();
                    transformBox.Controls[11].Text = selectedGO.GetComponent<Transform>().rotation.y.ToString();
                    transformBox.Controls[13].Text = selectedGO.GetComponent<Transform>().rotation.z.ToString();
                }
            }
            else
            {
                if (Input.GetKey("W"))
                {
                    editorCamera.position += editorCamera.Forward;
                }
                if (Input.GetKey("A"))
                {
                    editorCamera.position -= editorCamera.Right;
                }
                if (Input.GetKey("S"))
                {
                    editorCamera.position -= editorCamera.Forward;
                }
                if (Input.GetKey("D"))
                {
                    editorCamera.position += editorCamera.Right;
                }
            }
        }

        //Update Draw function
        private void EditorView_Paint(object sender, PaintEventArgs e)
        {
            Renderer.ScreenTarget = e.Graphics;
            Renderer.Render(Scene.currentScene.models, editorCamera);
        }
        private void GameView_Paint(object sender, PaintEventArgs e)
        {
            if (Camera.main != null)
            {
                labelNoCamera.Visible = false;
                Renderer.ScreenTarget = e.Graphics;
                fps += Renderer.Render(Scene.currentScene.models, Camera.main);
                fpsDelay++;

                if (fpsDelay == 10)
                {
                    labelFPS.Text = "MyLittleEngine   fps: " + Math.Round(fps / 10, 1);
                    fpsDelay = 0;
                    fps = 0;
                }
            }
            else
                labelNoCamera.Visible = true;
        }

        //Exporting
        private void ButtonExport_Click(object sender, EventArgs e)
        {
        }

        //Moving with mouse
        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            dx = mouseX - Cursor.Position.X;
            dy = mouseY - Cursor.Position.Y;

            if (letRotate)
            {
                Vector3 v = new Vector3(0, 0, 0)
                {
                    x = editorCamera.eulerAngles.x - dy * 0.005f,
                    y = editorCamera.eulerAngles.y - dx * 0.005f
                };

                editorCamera.eulerAngles = v;
            }
            mouseX = Cursor.Position.X;
            mouseY = Cursor.Position.Y;
        }
        private void TabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                letRotate = true;
                tabControl1.Focus();
            }
        }
        private void TabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) letRotate = false;
        }


        //Moving with WSAD
        private void TabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.SetKey(e, true);
        }
        private void TabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.SetKey(e, false);
        }





        //Selecting GameObject
        private void TreeViewGOs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (selectedGO == (GameObject)treeViewGOs.SelectedNode.Tag) return;

            selectedGO = (GameObject)treeViewGOs.SelectedNode.Tag;

            objectName.Text = selectedGO.Name;

            RedrawComponents();
        }
        private void RedrawComponents()
        {
            componentsLayout.Controls.Clear();

            foreach (Component comp in selectedGO.GetComponents<Component>())
            {
                if (comp is Script) DrawScriptComponent((Script)comp);
                else if (comp is Light) DrawLightComponent((Light)comp);
                else if (comp is MeshRenderer) DrawMeshRendererComponent((MeshRenderer)comp);
                else if (comp is Transform) DrawTransformComponent((Transform)comp);
                else if (comp is CameraComponent) DrawCameraComponent((CameraComponent)comp);
                else if (comp is BoxColliderComponent) DrawBoxColliderComponent((BoxColliderComponent)comp);
                else if (comp is RigidbodyComponent) DrawRigidbodyComponent((RigidbodyComponent)comp);
            }

            componentsLayout.Controls.Add(btnAddComponent);
        }


        //Context menu Hierarchy
        private void CreateGameObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameObject GO = new GameObject();
            GO.Name += treeViewGOs.Nodes.Count;
            TreeNode node = new TreeNode(GO.Name)
            {
                Tag = GO
            };
            treeViewGOs.Nodes.Add(node);

            Scene.currentScene.gameObjects.Add(GO);
        }

        private void CreateCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameObject GO = new GameObject();
            TreeNode node = new TreeNode(GO.Name + treeViewGOs.Nodes.Count)
            {
                Tag = GO
            };
            treeViewGOs.Nodes.Add(node);
            GO.AddComponent<CameraComponent>();

            Scene.currentScene.gameObjects.Add(GO);
        }

        //Removing GameObject
        private void TreeViewGOs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (treeViewGOs.SelectedNode != null)
                {
                    if (selectedGO.GetComponent<Transform>().model != null)
                        Scene.currentScene.models.Remove(selectedGO.GetComponent<Transform>().model);

                    foreach (Component c in selectedGO.GetComponents<Component>())
                    {
                        if ((c is Transform) == false) selectedGO.RemoveComponent(c);
                    }
                    selectedGO.RemoveComponentsOfType<Transform>();
                    Scene.currentScene.gameObjects.Remove(selectedGO);
                    treeViewGOs.Nodes.Remove(treeViewGOs.SelectedNode);

                }
            }
        }




        //Loading Scripts
        public bool CreateNewScript(string path)
        {
            if (File.Exists(originalPath + "\\Assembly.csproj"))
            {
                string[] assembly = File.ReadAllLines(originalPath + "\\Assembly.csproj");
                string[] newAssembly = new string[assembly.Length + 1];

                int j = 0;

                for (int i = 0; i < assembly.Length; i++)
                {
                    if (assembly[i] == "    <!--Scripts-->" || assembly[i] == "<!--Scripts-->")
                    {
                        newAssembly[i + j] = assembly[i];
                        j = 1;
                        newAssembly[i + j] = "<Compile Include=\"" + path + "\" />";
                    }
                    else
                    {
                        newAssembly[i + j] = assembly[i];
                    }
                }

                foreach (string line in newAssembly)
                {
                    Debug.WriteLine(line);
                }

                File.WriteAllLines(originalPath + "\\Assembly.csproj", newAssembly);

                return true;
            }
            else return false;
        }

        public void ResetContextMenu()
        {
            contextMenuComps.Items.Clear();

            contextMenuComps.Items.Add(lightToolStripMenuItem);
            contextMenuComps.Items.Add(meshRendererToolStripMenuItem);
            contextMenuComps.Items.Add(audioToolStripMenuItem);
            contextMenuComps.Items.Add(cameraToolStripMenuItem);
            contextMenuComps.Items.Add(boxColliderToolStripMenuItem);
            contextMenuComps.Items.Add(rigidbodyToolStripMenuItem);       
        }
        public void LoadScripts(bool isInitial)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            // Reference to System.Drawing library
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("MyLittleEngineV8.exe");
            parameters.ReferencedAssemblies.Add("MyLittleRendererV8.dll");
            parameters.ReferencedAssemblies.Add("MyLittlePhysics.dll");
            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = false;
            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = false;

            Random rand = new Random();

            Directory.CreateDirectory(originalPath + "\\temp");
            
            string name = projectName + rand.Next().ToString();

            if (isInitial)
            {
                name = File.ReadAllText(originalPath + "\\temp\\temp");
            }

            parameters.OutputAssembly = originalPath + "\\temp\\" + name + ".dll";

            string[] filePaths = Directory.GetFiles(originalPath, "*.cs", SearchOption.AllDirectories);
            string[] scripts;

            if (filePaths.Length == 0) return;
            else scripts = new string[filePaths.Length];

            for (int i = 0; i < filePaths.Length; i++)
            {
                scripts[i] = File.ReadAllText(filePaths[i]);
            }

            File.WriteAllText(originalPath + "\\temp\\temp", name);

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, scripts);


            if (results.Errors.HasErrors)
            {
                string sb = "";

                foreach (CompilerError error in results.Errors)
                {
                    sb += (String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }

            Assembly assembly = results.CompiledAssembly;

            userScripts.Clear();
            ResetContextMenu();


            foreach (Type type in assembly.GetTypes())
            {
                fileNames.Add(type.Name);

                Script s = (Script)Activator.CreateInstance(type);
                userScripts.Add(type.Name + ".cs", s);

                ToolStrip toolStrip = new ToolStrip();
                contextMenuComps.Items.Add(type.Name + ".cs");

            }

        }
      

        //Folders Control
        private void GetFilesList(string path)
        {
            listView.Clear();
            fileNames.Clear();

            foreach (string item in Directory.GetFiles(path))
            {
                imageList1.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(item));
                FileInfo info = new FileInfo(item);
                fileNames.Add(info.FullName);
                listView.Items.Add(info.Name, imageList1.Images.Count - 1);
            }
            foreach (string item in Directory.GetDirectories(path))
            {
                DirectoryInfo info = new DirectoryInfo(item);
                //fileNames.Add(info.FullName);
                listView.Items.Add(info.Name, 0);
            }
        }
        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                if (listView.FocusedItem.ImageIndex == 0)
                {
                    previousPath = currentPath;
                    currentPath = currentPath + "\\" + listView.FocusedItem.Text;
                    GetFilesList(currentPath);
                }
                else
                {
                    Process.Start(fileNames[listView.FocusedItem.Index]);
                }
            }
        }
        private void BtnBackFolder_Click(object sender, EventArgs e)
        {
            currentPath = previousPath;

            if (currentPath != originalPath + "\\Assets")
            {
                string[] splited = currentPath.Split('\\');

                string newPath = splited[0];

                for (int i = 1; i < splited.Length - 1; i++)
                {
                    newPath += "\\" + splited[i];
                }
                previousPath = newPath;
            }

            GetFilesList(currentPath);
        }




        //Choosing comp to create MENU
        private void LightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLightComponent();
        }
        private void MeshRendererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMeshRendererComponent(null);
        }
        private void ScriptToolStripMenuItem_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            string[] s = e.ClickedItem.Text.Split('.');
            string ext = s[s.Length - 1];
            if (ext == "cs")
            {
                AddScriptComponent(e.ClickedItem.Text);
            }
        }
        private void CameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCameraComponent();
        }
        private void BoxColliderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBoxColliderComponent();
        }
        private void RigidbodyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRigidbodyComponent();
        }


        // Set new values on COMPONENT DISPLAY if it changed
        private void TransformTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            float value;

            if (textBox.Text == "-") return;

            switch (textBox.Name)
            {
                case "posX":
                    if (float.TryParse(textBox.Text, out value))
                    {
                        selectedGO.GetComponent<Transform>().Translate(new Vector3(value - selectedGO.GetComponent<Transform>().position.x, 0, 0));
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<Transform>().position.x.ToString();
                    break;
                case "posY":
                    if (float.TryParse(textBox.Text, out value))
                        selectedGO.GetComponent<Transform>().Translate(new Vector3(0, value - selectedGO.GetComponent<Transform>().position.y, 0));
                    else
                        textBox.Text = selectedGO.GetComponent<Transform>().position.y.ToString();
                    break;
                case "posZ":
                    if (float.TryParse(textBox.Text, out value))
                        selectedGO.GetComponent<Transform>().Translate(new Vector3(0, 0, value - selectedGO.GetComponent<Transform>().position.z));
                    else
                        textBox.Text = selectedGO.GetComponent<Transform>().position.z.ToString();
                    break;
                case "rotX":
                    if (float.TryParse(textBox.Text, out value))
                    {
                        selectedGO.GetComponent<Transform>().Rotate(new Vector3(value - selectedGO.GetComponent<Transform>().rotation.x, 0, 0));
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<Transform>().position.x.ToString();
                    break;
                case "rotY":
                    if (float.TryParse(textBox.Text, out value))
                        selectedGO.GetComponent<Transform>().Rotate(new Vector3(0, value - selectedGO.GetComponent<Transform>().rotation.y, 0));
                    else
                        textBox.Text = selectedGO.GetComponent<Transform>().position.y.ToString();
                    break;
                case "rotZ":
                    if (float.TryParse(textBox.Text, out value))
                        selectedGO.GetComponent<Transform>().Rotate(new Vector3(0, 0, value - selectedGO.GetComponent<Transform>().rotation.z));
                    else
                        textBox.Text = selectedGO.GetComponent<Transform>().position.z.ToString();
                    break;
                default:
                    break;
            }
        }        
        private void CameraSettingsTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            float value;

            if (textBox.Text == "-") return;

            switch (textBox.Name)
            {
                case "ScreenHeight":
                    if (float.TryParse(textBox.Text, out value))
                    {
                        selectedGO.GetComponent<CameraComponent>().settings.ScreenHeight = (int)value;
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<CameraComponent>().settings.ScreenHeight.ToString();
                    break;
                case "FOV":
                    if (float.TryParse(textBox.Text, out value))
                        selectedGO.GetComponent<CameraComponent>().settings.FOV = value;
                    else
                        textBox.Text = selectedGO.GetComponent<CameraComponent>().settings.FOV.ToString();
                    break;
                case "farPlane":
                    if (float.TryParse(textBox.Text, out value))
                        selectedGO.GetComponent<CameraComponent>().settings.farPlane = value;
                    else
                        textBox.Text = selectedGO.GetComponent<CameraComponent>().settings.farPlane.ToString();
                    break;
                case "AspectRatio":
                    if (float.TryParse(textBox.Text, out value))
                        selectedGO.GetComponent<CameraComponent>().settings.AspectRatio = value;
                    else
                        textBox.Text = selectedGO.GetComponent<CameraComponent>().settings.AspectRatio.ToString();
                    break;
                case "ClippingDistance":
                    if (float.TryParse(textBox.Text, out value))
                    {
                        if (value > 0.1f) selectedGO.GetComponent<CameraComponent>().settings.ClippingDistance = value;
                        else
                        {
                            selectedGO.GetComponent<CameraComponent>().settings.ClippingDistance = 0.1f;
                            textBox.Text = "0,1";
                        }
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<CameraComponent>().settings.ClippingDistance.ToString();
                    break;
                default:
                    break;
            }
        }
        private void BoxColliderSettingsTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            float value;

            switch (textBox.Name)
            {
                case "SizeX":
                    if (float.TryParse(textBox.Text, out value))
                    {
                        selectedGO.GetComponent<BoxColliderComponent>().boxCollider.SizeX = (int)value;
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<BoxColliderComponent>().boxCollider.SizeX.ToString();
                    break;
                case "SizeY":
                    if (float.TryParse(textBox.Text, out value))
                        selectedGO.GetComponent<BoxColliderComponent>().boxCollider.SizeY = value;
                    else
                        textBox.Text = selectedGO.GetComponent<BoxColliderComponent>().boxCollider.SizeY.ToString();
                    break;
                case "SizeZ":
                    if (float.TryParse(textBox.Text, out value))
                        selectedGO.GetComponent<BoxColliderComponent>().boxCollider.SizeZ = value;
                    else
                        textBox.Text = selectedGO.GetComponent<BoxColliderComponent>().boxCollider.SizeZ.ToString();
                    break;              
                default:
                    break;
            }
        }
        private void ModelColorChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            byte value;

            switch (textBox.Name)
            {
                case "colA":
                    if (byte.TryParse(textBox.Text, out value))
                    {
                        selectedGO.GetComponent<MeshRenderer>().A = (byte)value;
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<MeshRenderer>().A.ToString();
                    break;
                case "colR":
                    if (byte.TryParse(textBox.Text, out value))
                    {
                        selectedGO.GetComponent<MeshRenderer>().R = (byte)value;
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<MeshRenderer>().R.ToString();
                    break;
                case "colG":
                    if (byte.TryParse(textBox.Text, out value))
                    {
                        selectedGO.GetComponent<MeshRenderer>().G = (byte)value;
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<MeshRenderer>().G.ToString();
                    break;
                case "colB":
                    if (byte.TryParse(textBox.Text, out value))
                    {
                        selectedGO.GetComponent<MeshRenderer>().B = (byte)value;
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<MeshRenderer>().B.ToString();
                    break;
                default:
                    break;
            }
        }

        private void RigidbodySettingsTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            float value;

            switch (textBox.Name)
            {
                case "Mass":
                    if (float.TryParse(textBox.Text, out value))
                    {
                        selectedGO.GetComponent<RigidbodyComponent>().Rigidbody.Mass = value;
                    }
                    else
                        textBox.Text = selectedGO.GetComponent<RigidbodyComponent>().Rigidbody.Mass.ToString();
                    break;
               
                default:
                    break;
            }

        }


        // Transform Component
        private void DrawTransformComponent(Transform transform)
        {
            componentsLayout.Controls.Remove(btnAddComponent);
            GroupBox box = new GroupBox
            {
                Text = "Transform",
                Width = componentsLayout.Width - 40,
                Height = 120
            };
            componentsLayout.Controls.Add(box);

            #region Position text creator

            Label pos = new Label
            {
                Text = "Position",
                Width = 70,
                Location = new Point(10, 28),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };

            Label pX = new Label
            {
                Text = "X",
                Location = new Point(85, 30),
                Width = 10
            };

            TextBox pXvalue = new TextBox
            {
                Name = "posX",
                Text = transform.position.x.ToString(),
                Width = 70,
                Location = new Point(100, 26)
            };
            pXvalue.TextChanged += TransformTextChanged;

            Label pY = new Label
            {
                Text = "Y",
                Location = new Point(175, 30),
                Width = 10
            };

            TextBox pYvalue = new TextBox
            {
                Name = "posY",
                Text = transform.position.y.ToString(),
                Width = 70,
                Location = new Point(190, 26)
            };
            pYvalue.TextChanged += TransformTextChanged;

            Label pZ = new Label
            {
                Text = "Z",
                Location = new Point(265, 30),
                Width = 10
            };

            TextBox pZvalue = new TextBox
            {
                Name = "posZ",
                Text = transform.position.z.ToString(),
                Width = 70,
                Location = new Point(280, 26)
            };
            pZvalue.TextChanged += TransformTextChanged;



            box.Controls.Add(pos);
            box.Controls.Add(pX);
            box.Controls.Add(pXvalue);
            box.Controls.Add(pY);
            box.Controls.Add(pYvalue);
            box.Controls.Add(pZ);
            box.Controls.Add(pZvalue);

            #endregion

            #region Rotation text creator

            Label rot = new Label
            {
                Text = "Rotation",
                Width = 70,
                Location = new Point(10, 58),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };

            Label rX = new Label
            {
                Text = "X",
                Location = new Point(85, 60),
                Width = 10
            };

            TextBox rXvalue = new TextBox
            {
                Name = "rotX",
                Text = transform.rotation.x.ToString(),
                Width = 70,
                Location = new Point(100, 56)
            };
            rXvalue.TextChanged += TransformTextChanged;

            Label rY = new Label
            {
                Text = "Y",
                Location = new Point(175, 60),
                Width = 10
            };

            TextBox rYvalue = new TextBox
            {
                Name = "rotY",
                Text = transform.rotation.y.ToString(),
                Width = 70,
                Location = new Point(190, 56)
            };
            rYvalue.TextChanged += TransformTextChanged;

            Label rZ = new Label
            {
                Text = "Z",
                Location = new Point(265, 60),
                Width = 10
            };

            TextBox rZvalue = new TextBox
            {
                Name = "rotZ",
                Text = transform.rotation.z.ToString(),
                Width = 70,
                Location = new Point(280, 56)
            };
            rZvalue.TextChanged += TransformTextChanged;



            box.Controls.Add(rot);
            box.Controls.Add(rX);
            box.Controls.Add(rXvalue);
            box.Controls.Add(rY);
            box.Controls.Add(rYvalue);
            box.Controls.Add(rZ);
            box.Controls.Add(rZvalue);

            #endregion

            #region Scale text creator

            Label scale = new Label
            {
                Text = "Scale",
                Width = 70,
                Location = new Point(10, 88),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };

            Label sX = new Label
            {
                Text = "X",
                Location = new Point(85, 90),
                Width = 10
            };

            TextBox sXvalue = new TextBox
            {
                Text = "0",
                Width = 70,
                Location = new Point(100, 86)
            };

            Label sY = new Label
            {
                Text = "Y",
                Location = new Point(175, 90),
                Width = 10
            };

            TextBox sYvalue = new TextBox
            {
                Text = "0",
                Width = 70,
                Location = new Point(190, 86)
            };

            Label sZ = new Label
            {
                Text = "Z",
                Location = new Point(265, 90),
                Width = 10
            };

            TextBox sZvalue = new TextBox
            {
                Text = "0",
                Width = 70,
                Location = new Point(280, 86)
            };



            box.Controls.Add(scale);
            box.Controls.Add(sX);
            box.Controls.Add(sXvalue);
            box.Controls.Add(sY);
            box.Controls.Add(sYvalue);
            box.Controls.Add(sZ);
            box.Controls.Add(sZvalue);

            #endregion


            componentsLayout.Controls.Add(btnAddComponent);

            transformBox = box;
        }

        // AddBoxColliderComponent
        private void AddBoxColliderComponent()
        {
            if (selectedGO == null) return;

            selectedGO.AddComponent<BoxColliderComponent>();

            selectedGO.GetComponent<BoxColliderComponent>().boxCollider.SetPosition(selectedGO.GetComponent<Transform>().position);
            selectedGO.GetComponent<BoxColliderComponent>().boxCollider.gameObject = selectedGO;
            selectedGO.GetComponent<BoxColliderComponent>().boxCollider.Name = selectedGO.Name;
            selectedGO.GetComponent<BoxColliderComponent>().gameObject = selectedGO;

            foreach (Script s in selectedGO.GetComponents<Script>())
            {
                selectedGO.GetComponent<BoxColliderComponent>().boxCollider.OnCollision += s.OnCollision;
            }

            DrawBoxColliderComponent(selectedGO.GetComponent<BoxColliderComponent>());
        }
        private void DrawBoxColliderComponent(BoxColliderComponent colliderComponent)
        {
            componentsLayout.Controls.Remove(btnAddComponent);

            GroupBox box = new GroupBox
            {
                Text = "Box Collider",
                Width = componentsLayout.Width - 40,
                Height = 100
            };
            componentsLayout.Controls.Add(box);

            Label pos = new Label
            {
                Text = "Size",
                Width = 70,
                Location = new Point(10, 28),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };

            Label pX = new Label
            {
                Text = "X",
                Location = new Point(85, 30),
                Width = 10
            };

            TextBox pXvalue = new TextBox
            {
                Name = "SizeX",
                Text = colliderComponent.boxCollider.SizeX.ToString(),
                Width = 70,
                Location = new Point(100, 26)
            };
            pXvalue.TextChanged += BoxColliderSettingsTextChanged;

            Label pY = new Label
            {
                Text = "Y",
                Location = new Point(175, 30),
                Width = 10
            };

            TextBox pYvalue = new TextBox
            {
                Name = "SizeY",
                Text = colliderComponent.boxCollider.SizeY.ToString(),
                Width = 70,
                Location = new Point(190, 26)
            };
            pYvalue.TextChanged += BoxColliderSettingsTextChanged;

            Label pZ = new Label
            {
                Text = "Z",
                Location = new Point(265, 30),
                Width = 10
            };

            TextBox pZvalue = new TextBox
            {
                Name = "SizeZ",
                Text = colliderComponent.boxCollider.SizeZ.ToString(),
                Width = 70,
                Location = new Point(280, 26)
            };
            pZvalue.TextChanged += BoxColliderSettingsTextChanged;

            Label checkBoxLabel = new Label
            {
                Text = "Is Trigger",
                Location = new Point(10, 68),
                Font = new Font("Arial", 10, FontStyle.Regular),
                Width = 70
            };

            CheckBox checkBox = new CheckBox
            {
                Location = new Point(110, 66),
                Tag = colliderComponent
            };
            checkBox.CheckedChanged += CheckBoxisTrigger;

            box.Controls.Add(pos);
            box.Controls.Add(pX);
            box.Controls.Add(pXvalue);
            box.Controls.Add(pY);
            box.Controls.Add(pYvalue);
            box.Controls.Add(pZ);
            box.Controls.Add(pZvalue);
            box.Controls.Add(pZ);
            box.Controls.Add(pZvalue);
            box.Controls.Add(checkBox);
            box.Controls.Add(checkBoxLabel);

            CreateDeleteButton(box, colliderComponent);

            componentsLayout.Controls.Add(btnAddComponent);
        }


        // AddRigidbodyComponent
        private void AddRigidbodyComponent()
        {
            if (selectedGO == null) return;

            selectedGO.AddComponent<RigidbodyComponent>();
    
            selectedGO.GetComponent<RigidbodyComponent>().gameObject = selectedGO;          

            DrawRigidbodyComponent(selectedGO.GetComponent<RigidbodyComponent>());
        }
        private void DrawRigidbodyComponent(RigidbodyComponent rigidbodyComponent)
        {
            componentsLayout.Controls.Remove(btnAddComponent);

            GroupBox box = new GroupBox
            {
                Text = "Rigidbody",
                Width = componentsLayout.Width - 40,
                Height = 100
            };
            componentsLayout.Controls.Add(box);

            Label pos = new Label
            {
                Text = "Mass",
                Width = 70,
                Location = new Point(10, 28),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
                   

            TextBox pXvalue = new TextBox
            {
                Name = "Mass",
                Text = rigidbodyComponent.Rigidbody.Mass.ToString(),
                Width = 70,
                Location = new Point(100, 26)
            };
            pXvalue.TextChanged += RigidbodySettingsTextChanged;

           
            Label checkBoxLabel = new Label
            {
                Text = "Is Kinetic",
                Location = new Point(10, 68),
                Font = new Font("Arial", 10, FontStyle.Regular),
                Width = 70
            };

            CheckBox checkBox = new CheckBox
            {
                Location = new Point(110, 66),
                Tag = rigidbodyComponent
            };
            checkBox.CheckedChanged += CheckBoxisTrigger; // To do

            box.Controls.Add(pos);
            box.Controls.Add(pXvalue);
            box.Controls.Add(checkBox);
            box.Controls.Add(checkBoxLabel);

            CreateDeleteButton(box, rigidbodyComponent);

            componentsLayout.Controls.Add(btnAddComponent);
        }

        // CameraComponent
        private void AddCameraComponent()
        {
            if (selectedGO == null) return;

            selectedGO.AddComponent<CameraComponent>();

            DrawCameraComponent(selectedGO.GetComponent<CameraComponent>());
        }
        private void DrawCameraComponent(CameraComponent camera)
        {
            componentsLayout.Controls.Remove(btnAddComponent);

            GroupBox box = new GroupBox
            {
                Text = "Camera settings",
                Width = componentsLayout.Width - 40
            };
            componentsLayout.Controls.Add(box);

            Type myType = camera.settings.GetType();
            FieldInfo[] myField = myType.GetFields();

            int yPos = 30;

            foreach (FieldInfo field in myField)
            {
                Label fieldName = new Label
                {
                    Text = field.Name,
                    Width = 150,
                    Location = new Point(10, yPos)
                };

                TextBox fieldValue = new TextBox
                {
                    Name = field.Name,
                    Height = 20,
                    Location = new Point(170, yPos),
                    Text = field.GetValue(camera.settings).ToString()
                };
                fieldValue.TextChanged += CameraSettingsTextChanged;

                box.Controls.Add(fieldName);
                box.Controls.Add(fieldValue);

                yPos += 30;
            }

            Label labelCheckbox = new Label
            {
                Text = "Active",
                Width = 150,
                Location = new Point(10, yPos)
            };

            CheckBox checkBox = new CheckBox
            {
                Location = new Point(170, yPos),
                Tag = camera
            };
            checkBox.CheckedChanged += CheckBoxisMainCamera;
            if (Camera.main == camera.camera)
                checkBox.Checked = true;
            else
                checkBox.Checked = false;

            box.Height = yPos + 40;

            box.Controls.Add(labelCheckbox);
            box.Controls.Add(checkBox);

            CreateDeleteButton(box, camera);
            componentsLayout.Controls.Add(btnAddComponent);
        }

        //ScriptComponent
        private void AddScriptComponent(string scriptName)
        {
            if (selectedGO == null) return;
            userScripts.TryGetValue(scriptName, out Script s);
            Script sCloned = s.Clone();
            sCloned.Name = scriptName;
            selectedGO.AddComponent<Script>(ref sCloned);

            DrawScriptComponent(sCloned);
        }
        private void DrawScriptComponent(Script script)
        {
            // Remove and add button 'Add' so it always stays at bottom
            componentsLayout.Controls.Remove(btnAddComponent);

            GroupBox box = new GroupBox
            {
                Text = script.GetType().ToString(),
                Width = componentsLayout.Width - 40
            };
            componentsLayout.Controls.Add(box);


            FlowLayoutPanel scriptPanel = new FlowLayoutPanel
            {
                Width = componentsLayout.Width - 60,
                Location = new System.Drawing.Point(5, 30)
            };
            box.Controls.Add(scriptPanel);


            Type myType = script.GetType();
            FieldInfo[] myField = myType.GetFields();

            foreach (FieldInfo field in myField)
            {
                if (field.Name != "gameObject")
                {
                    Label fieldName = new Label
                    {
                        Text = field.Name,
                        Width = 150
                    };

                    TextBox fieldValue = new TextBox
                    {
                        Height = 20,
                        Width = 100
                    };
                    try
                    {
                        fieldValue.Text = field.GetValue(script).ToString();

                        if (field.FieldType == typeof(GameObject))
                        {
                            GameObject go = field.GetValue(script) as GameObject;
                            fieldValue.Text = go.Name + "(" + field.FieldType + ")";
                        }
                    }
                    catch
                    {
                    }
                    fieldValue.ReadOnly = true;
                    fieldValue.DragEnter += DragEnter_fieldValue;
                    fieldValue.DragDrop += DragDrop_fieldValue;
                    fieldValue.Name = field.Name;
                    fieldValue.Tag = script;
                    fieldValue.AllowDrop = true;


                    scriptPanel.Controls.Add(fieldName);
                    scriptPanel.Controls.Add(fieldValue);
                }
            }
            box.Height = scriptPanel.Height + 5;
            scriptPanel.Height -= 30;

            CreateDeleteButton(box, script);
            componentsLayout.Controls.Add(btnAddComponent);
        }
        private void DragDrop_fieldValue(object sender, DragEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Type type = textBox.Tag.GetType();
            FieldInfo field = type.GetField(textBox.Name);

            if (field.FieldType == typeof(GameObject))
            {
                field.SetValue(textBox.Tag, draggedGO);
                textBox.Text = draggedGO.Name;
                return;
            }

            foreach (Component c in draggedGO.GetComponents<Component>())
            {
                if (c.GetType() == field.FieldType)
                {
                    field.SetValue(textBox.Tag, c);
                    textBox.Text = draggedGO.Name + "(" + field.FieldType +")";
                    break;
                }
            }
        }
        private void DragEnter_fieldValue(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //MeshRendererComponent
        private void DrawMeshRendererComponent(MeshRenderer meshRenderer)
        {
            componentsLayout.Controls.Remove(btnAddComponent);
            GroupBox box = new GroupBox
            {
                Text = "Mesh Renderer",
                Width = componentsLayout.Width - 40,
                Height = 210
            };
            componentsLayout.Controls.Add(box);



            Label fieldMesh_name = new Label
            {
                Text = "   Mesh:",
                Width = 50,
                Location = new System.Drawing.Point(10, 30)
            };



            TextBox fieldValueM = new TextBox
            {
                Text = selectedGO.GetComponent<MeshRenderer>().MeshPath,
                Width = 200,
                Location = new System.Drawing.Point(70, 28)
            };



            Label fieldTexture_name = new Label
            {
                Text = "Texture:",
                Width = 50,
                Location = new System.Drawing.Point(10, 70)
            };



            TextBox fieldValueT = new TextBox
            {
                Text = selectedGO.GetComponent<MeshRenderer>().TexturePath,
                Width = 200,
                Location = new System.Drawing.Point(70, 68)
            };


            Label pos = new Label
            {
                Text = "Color",
                Width = 70,
                Location = new Point(10, 128),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };

            Label pA = new Label
            {
                Text = "A",
                Location = new Point(85, 150),
                Width = 10
            };

            TextBox pAvalue = new TextBox
            {
                Name = "colA",
                Text = meshRenderer.Model.Color.A.ToString(),
                Width = 70,
                Location = new Point(100, 146)
            };
            pAvalue.TextChanged += ModelColorChanged;

            Label pX = new Label
            {
                Text = "R",
                Location = new Point(85, 130),
                Width = 10
            };

            TextBox pXvalue = new TextBox
            {
                Name = "colR",
                Text = meshRenderer.Model.Color.R.ToString(),
                Width = 70,
                Location = new Point(100, 126)
            };
            pXvalue.TextChanged += ModelColorChanged;

            Label pY = new Label
            {
                Text = "G",
                Location = new Point(175, 130),
                Width = 10
            };

            TextBox pYvalue = new TextBox
            {
                Name = "colG",
                Text = meshRenderer.Model.Color.G.ToString(),
                Width = 70,
                Location = new Point(190, 126)
            };
            pYvalue.TextChanged += ModelColorChanged;

            Label pZ = new Label
            {
                Text = "B",
                Location = new Point(265, 130),
                Width = 10
            };

            TextBox pZvalue = new TextBox
            {
                Name = "colB",
                Text = meshRenderer.Model.Color.B.ToString(),
                Width = 70,
                Location = new Point(280, 126)
            };
            pZvalue.TextChanged += ModelColorChanged;


            box.Controls.Add(pos);
            box.Controls.Add(pX);
            box.Controls.Add(pXvalue);
            box.Controls.Add(pY);
            box.Controls.Add(pYvalue);
            box.Controls.Add(pZ);
            box.Controls.Add(pZvalue);
            box.Controls.Add(pA);
            box.Controls.Add(pAvalue);
            box.Controls.Add(fieldMesh_name);
            box.Controls.Add(fieldTexture_name);
            box.Controls.Add(fieldValueM);
            box.Controls.Add(fieldValueT);

            CreateDeleteButton(box, meshRenderer);

            componentsLayout.Controls.Add(btnAddComponent);
        }
        private void AddMeshRendererComponent(string meshPath)
        {
            if (selectedGO == null) return;

            if (selectedGO.GetComponent<MeshRenderer>() == null)
            {
                selectedGO.AddComponent<MeshRenderer>();
                selectedGO.GetComponent<MeshRenderer>().Model.LoadFromObj(meshPath);
                selectedGO.GetComponent<MeshRenderer>().MeshPath = meshPath;
                //mr.TexturePath = texturePath;
                selectedGO.GetComponent<Transform>().model = selectedGO.GetComponent<MeshRenderer>().Model;

                selectedGO.GetComponent<Transform>().model.Move(selectedGO.GetComponent<Transform>().position);
                selectedGO.GetComponent<Transform>().model.Rotate(selectedGO.GetComponent<Transform>().rotation);

                DrawMeshRendererComponent(selectedGO.GetComponent<MeshRenderer>());
            }
            else
            {
                selectedGO.GetComponent<MeshRenderer>().Model.LoadFromObj(meshPath);
                selectedGO.GetComponent<MeshRenderer>().MeshPath = meshPath;
                //mr.TexturePath = texturePath;
                selectedGO.GetComponent<Transform>().model = selectedGO.GetComponent<MeshRenderer>().Model;

                selectedGO.GetComponent<Transform>().model.Move(selectedGO.GetComponent<Transform>().position);
                selectedGO.GetComponent<Transform>().model.Rotate(selectedGO.GetComponent<Transform>().rotation);
            }
        }

        //LightComponent
        private void AddLightComponent()
        {
            if (selectedGO == null) return;
            selectedGO.AddComponent<Light>();
            DrawLightComponent(selectedGO.GetComponent<Light>());

        }
        private void DrawLightComponent(Light light)
        {
            componentsLayout.Controls.Remove(btnAddComponent);
            GroupBox box = new GroupBox
            {
                Text = "Light",
                Width = componentsLayout.Width - 40,
                Height = 50
            };
            //light.Location.X = 10; 
            componentsLayout.Controls.Add(box);
            CreateDeleteButton(box, light);

            componentsLayout.Controls.Add(btnAddComponent);
        }

        //Deleting button
        private void CreateDeleteButton(GroupBox box, Component component)
        {
            //Creating delete Component button

            Button btnDelete = new Button
            {
                Text = "X",
                Width = 20,
                Height = 20,
                Location = new Point(box.Width - 18, 4),
                Tag = component
            };
            btnDelete.Click += DeleteComponent;

            box.Controls.Add(btnDelete);
        }
        private void DeleteComponent(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            selectedGO.RemoveComponent((Component)btn.Tag);

            if ((Component)btn.Tag is MeshRenderer) Scene.currentScene.models.Remove(selectedGO.GetComponent<Transform>().model);
            else if ((Component)btn.Tag is CameraComponent)
            {
                CameraComponent c = btn.Tag as CameraComponent;
                if (c.camera == Camera.main) Camera.main = null;
            }

            btn.Tag = null;
            RedrawComponents();
        }





        //Start, stop Game mode 
        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            if (playMode)
            {
                playMode = false;
                btnStartStop.Text = "Start";

                BackColor = Color.FromArgb(255, 227, 227, 227);
            }
            else
            {
                playMode = true;
                StartFunctions();
                btnStartStop.Text = "Stop";

                BackColor = Color.DarkGray;
            }
        }
        //Opening list of components to Add
        private void BtnAddComponent_Click(object sender, EventArgs e)
        {
            int x = btnAddComponent.Location.X + Inspector.Location.X + this.Location.X + 110;
            int y = btnAddComponent.Location.Y + Inspector.Location.Y + this.Location.Y + 120;
            contextMenuComps.Show(new Point(x, y));
        }
        //Setting main camera
        private void CheckBoxisMainCamera(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            CameraComponent c = (CameraComponent)box.Tag;
            if (box.Checked) Camera.main = c.camera;
            else Camera.main = null;
        }
        private void CheckBoxisTrigger(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            BoxColliderComponent c = (BoxColliderComponent)box.Tag;
            if (box.Checked) c.boxCollider.isTrigger = true;
            else c.boxCollider.isTrigger = false;
        }

        //Drag drop components
        private void TreeViewGOs_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            draggedGO = (GameObject)e.Node.Tag;
        }
        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView.DoDragDrop(listView.SelectedItems[0], DragDropEffects.Move);
        }
        private void GameObject_ItemDrag(object sender, ItemDragEventArgs e)
        {
            treeViewGOs.DoDragDrop(draggedGO, DragDropEffects.Move);
        }
        private void ComponentsLayout_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void TreeViewGOs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }


        // Save as PREFAB
        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            if (selectedGO == null) return;

            selectedGO.SaveAsPrefab(currentPath +"\\"+ selectedGO.Name + ".prefab", true);
        }
        private void ComponentsLayout_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem).ToString());

            if (item == null) return;

            string[] s = item.Text.Split('.');
            string ext = s[s.Length - 1];

            if (ext == "obj")
            {
                AddMeshRendererComponent(fileNames[item.Index]);
            }
        }

        // Load PREFAB
        private void TreeViewGOs_DragDrop(object sender, DragEventArgs e)
        {
            if (!(e.Data.GetData(typeof(ListViewItem).ToString()) is ListViewItem item)) return;

            string path = fileNames[listView.Items.IndexOf(item)];

            try
            {
                GameObject go = GameObject.LoadPrefab(path);

                if (go.GetComponent<MeshRenderer>() != null)
                {
                    if (go.GetComponent<MeshRenderer>().Model != null)
                    {
                        Scene.currentScene.models.Add(go.GetComponent<MeshRenderer>().Model);
                    }
                }
                TreeNode treeNode = new TreeNode(go.Name)
                {
                    Tag = go
                };
                Scene.currentScene.gameObjects.Add(go);
                treeViewGOs.Nodes.Add(treeNode);

                foreach (Component c in go.GetComponents<Component>())
                {
                    c.gameObject = go;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }
        private void CompileUserScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadScripts(false);


            foreach (GameObject go in Scene.currentScene.gameObjects)
            {
                Script[] go_scripts = go.GetComponents<Script>();

                for (int i = 0; i < go_scripts.Length; i++)
                {
                    foreach (KeyValuePair<string, Script> us in userScripts)
                    {
                        Debug.WriteLine(go_scripts[i].Name);

                        if (go_scripts[i].Name == us.Key)
                        {
                            go.RemoveComponent(go_scripts[i]);
                            Script ss = us.Value.Clone();
                            RedrawComponents();
                            //go.AddComponent<Script>(ref ss);
                            //Debug.WriteLine("zamiana");
                        }
                    }
                }
            }

        }

        // Save scene
        private void SaveScene(string name)
        {
            string sceneText = "";

            foreach (GameObject go in Scene.currentScene.gameObjects)
            {
                sceneText += go.SaveAsPrefab(null, false);
                sceneText += "\n*\n";
            }

            File.WriteAllText(name, sceneText);
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentSceneName == null || currentSceneName == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    SaveScene(saveFileDialog1.FileName);
                    currentSceneName = saveFileDialog1.FileName;
                }
            }
            else
                SaveScene(currentSceneName);
        }
        private void OpenSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                currentSceneName = path;
            }
            else return;


            treeViewGOs.Nodes.Clear();
            Scene.currentScene.gameObjects.Clear();
            Scene.currentScene.models.Clear();


            string[] prefabs = File.ReadAllText(path).Split('*');

            foreach (string prefab in prefabs)
            {
                if (prefab != "" && prefab != "\n")
                {
                    GameObject go = GameObject.LoadPrefabFromText(prefab);
                    Scene.currentScene.gameObjects.Add(go);

                    if (go.GetComponent<MeshRenderer>() != null)
                    {
                        if (go.GetComponent<MeshRenderer>().Model != null)
                        {
                            Scene.currentScene.models.Add(go.GetComponent<MeshRenderer>().Model);
                            go.GetComponent<MeshRenderer>().Model.Move(go.GetComponent<Transform>().position);
                            go.GetComponent<MeshRenderer>().Model.Rotate(go.GetComponent<Transform>().rotation);
                        }
                    }
                    TreeNode treeNode = new TreeNode(go.Name)
                    {
                        Tag = go
                    };
                    treeViewGOs.Nodes.Add(treeNode);
                }
            }

            SetScriptsGameObjects();
        }

        private void SetScriptsGameObjects()
        {
            foreach (GameObject go in Scene.currentScene.gameObjects)
            { 
                foreach (Script script in go.GetComponents<Script>())
                {
                    foreach (FieldInfo field in script.GetType().GetFields())
                    {
                        if (field.FieldType == typeof(GameObject))
                        {                            
                            foreach (KeyValuePair<string,int> GO_ID in script.go_ids)
                            {
                                if (GO_ID.Key == field.Name)
                                {
                                    if(GO_ID.Value != 0)
                                        field.SetValue(script, Scene.currentScene.GetGameObjectOfIndex(GO_ID.Value));
                                }
                            }
                        }                        
                    }
                }
            }
        }

        private void ObjectName_TextChanged(object sender, EventArgs e)
        {
            selectedGO.Name = objectName.Text;
            treeViewGOs.SelectedNode.Text = objectName.Text;
        }

        private void NewSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scene.currentScene.models.Clear();
            Scene.currentScene.gameObjects.Clear();
            treeViewGOs.Nodes.Clear();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveScene(saveFileDialog1.FileName);
                currentSceneName = saveFileDialog1.FileName;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string name = scriptNameText.Text;

            if (!CreateNewScript(currentPath + "\\" + name + ".cs")) return;

            string scriptText =
                @"using System;
using System.Collections;
using System.Collections.Generic;
using MyLittleRendererV8;
using MyLittleEngineV8;

public class " + name + @" : Script
{
    // Use this for initialization
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {

    }
}";

            File.WriteAllText(currentPath + "\\" + name + ".cs", scriptText);

            panel1.Visible = false;
        }
    }
}