using System;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyLittleEngineV8
{
    public partial class HubForm : Form
    {
        private List<Project> Projects;

        public HubForm()
        {
            InitializeComponent();
        }

        private void HubForm_Load(object sender, EventArgs e)
        {
            panel1.Hide();

            if (File.Exists(@"..\projects"))
            {
                Projects = JsonConvert.DeserializeObject<List<Project>>(File.ReadAllText(@"..\projects"));
                if (Projects == null) Projects = new List<Project>();
            }
            else
            {
                File.WriteAllText(@"..\projects", null);
                Projects = new List<Project>();
            }

            LoadProjects();
        }

        private void LoadProjects()
        {
            projectsFlowPanel.Controls.Clear();

            foreach (Project p in Projects)
            {
                Button btn = new Button();
                projectsFlowPanel.Controls.Add(btn);
                btn.Tag = p;
                btn.Click += BtnOpenProject_Click;


                btn.Width = projectsFlowPanel.Width - 30;
                btn.Height = 80;
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.White;
                btn.FlatAppearance.BorderColor = Color.DarkGray;


                Button deleteBtn = new Button
                {
                    BackColor = System.Drawing.SystemColors.ControlLightLight,
                    FlatStyle = System.Windows.Forms.FlatStyle.Popup,
                    Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                    Location = new System.Drawing.Point(btn.Width - 35, 5),
                    Size = new System.Drawing.Size(29, 23),
                    Text = "X",
                    UseVisualStyleBackColor = false,
                    Tag = p
                };
                deleteBtn.Click += DeleteProject;
                btn.Controls.Add(deleteBtn);

                Label pName = new Label
                {
                    BackColor = Color.Transparent,
                    Enabled = false,
                    Width = projectsFlowPanel.Width - 60,
                    Text = p.Name,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    Location = new Point(20, btn.Height / 5)
                };
                btn.Controls.Add(pName);

                Label pPath = new Label
                {
                    Text = p.Path,
                    Enabled = false,
                    BackColor = Color.Transparent,
                    Width = projectsFlowPanel.Width - 60,
                    Location = new Point(20, (btn.Height / 2) + 10),
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    ForeColor = Color.Gray
                };
                btn.Controls.Add(pPath);

            }
        }

        // Open existing project
        private void BtnOpenProject_Click(object sender, EventArgs e)
        {
            Close();
            MainForm form = new MainForm();
            Project p = (Project)((Button)sender).Tag;
            form.currentPath  = p.Path + "\\" + p.Name + "\\Assets";
            form.previousPath = p.Path + "\\" + p.Name + "\\Assets";
            form.originalPath = p.Path + "\\" + p.Name ;
            form.projectName = p.Name;


            Program.mainForm = form;
        }

        // Show Add new Panel
        private void BtnNew_Click(object sender, EventArgs e)
        {
            panel1.Show();

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            textBox2.Text = path;
        }

        // Select folder
        private void Button4_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select project location" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = fbd.SelectedPath;
                }
            }
        }

        // Add new 
        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Project project = new Project
                {
                    Name = textBox1.Text,
                    Path = textBox2.Text
                };

                Projects.Add(project);

                var serializedObject = JsonConvert.SerializeObject(Projects, Formatting.Indented);
                File.WriteAllText(@"..\projects", serializedObject);

                Directory.CreateDirectory(project.Path + "\\" + project.Name);
                Directory.CreateDirectory(project.Path + "\\" + project.Name + "\\Assets");

                label5.Text = "";
                panel1.Hide();

                CreateCsProject(project.Name, project.Path);
                LoadProjects();
            }
            else
            {
                label5.Text = "Project name must be set";
            }
        }

        // Close Add new 
        private void Button3_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            label5.Text = "";
        }

        private void DeleteProject(object sender, EventArgs e)
        {
            Project p = ((Button)sender).Tag as Project;
            Projects.Remove(p);
                       
            var serializedObject = JsonConvert.SerializeObject(Projects, Formatting.Indented);
            File.WriteAllText(@"..\projects", serializedObject);

            LoadProjects();
        }
        private void CreateCsProject(string name, string path)
        {
            Directory.CreateDirectory(path + "\\" + name + "\\bin\\Debug");
            File.Copy("..\\Debug\\MyLittleEngineV8.exe", path + "\\" + name + "\\bin\\Debug\\MyLittleEngineV8.exe");
            File.Copy(@"..\Debug\MyLittleRendererV8.dll", path + "\\" + name + "\\bin\\Debug\\MyLittleRendererV8.dll");
            File.Copy(@"..\Debug\MyLittlePhysics.dll", path + "\\" + name + "\\bin\\Debug\\MyLittlePhysics.dll");
            File.Copy(@"..\Assembly.csproj", path + "\\" + name + "\\Assembly.csproj");

            #region Compiling csproject

            string projectFileName = path + "\\" + name + "\\Assembly.csproj";

            ProjectCollection pc = new ProjectCollection();

            Dictionary<string, string> GlobalProperty = new Dictionary<string, string>();

            GlobalProperty.Add("Configuration", "Debug");

            GlobalProperty.Add("Platform", "AnyCPU");

            BuildRequestData BuidlRequest = new BuildRequestData(projectFileName, GlobalProperty, null, new string[] { "Build" }, null);


            BuildResult buildResult = BuildManager.DefaultBuildManager.Build(new BuildParameters(pc), BuidlRequest);

            #endregion
        }
    }
    
}

