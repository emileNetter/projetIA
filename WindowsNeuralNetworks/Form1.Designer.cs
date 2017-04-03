namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxnbentrees = new System.Windows.Forms.TextBox();
            this.textBoxnbcouches = new System.Windows.Forms.TextBox();
            this.textBoxnbneurcouche = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBoxnumneur = new System.Windows.Forms.TextBox();
            this.textBoxnumcouche = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxnbiter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxalpha = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(53, 188);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "Init réseau";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(354, 182);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(170, 58);
            this.button2.TabIndex = 1;
            this.button2.Text = "apprentissage";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxnbentrees
            // 
            this.textBoxnbentrees.Location = new System.Drawing.Point(195, 7);
            this.textBoxnbentrees.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxnbentrees.Name = "textBoxnbentrees";
            this.textBoxnbentrees.Size = new System.Drawing.Size(100, 22);
            this.textBoxnbentrees.TabIndex = 3;
            this.textBoxnbentrees.Text = "2";
            // 
            // textBoxnbcouches
            // 
            this.textBoxnbcouches.Location = new System.Drawing.Point(195, 34);
            this.textBoxnbcouches.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxnbcouches.Name = "textBoxnbcouches";
            this.textBoxnbcouches.Size = new System.Drawing.Size(100, 22);
            this.textBoxnbcouches.TabIndex = 4;
            this.textBoxnbcouches.Text = "3";
            // 
            // textBoxnbneurcouche
            // 
            this.textBoxnbneurcouche.Location = new System.Drawing.Point(195, 63);
            this.textBoxnbneurcouche.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxnbneurcouche.Name = "textBoxnbneurcouche";
            this.textBoxnbneurcouche.Size = new System.Drawing.Size(100, 22);
            this.textBoxnbneurcouche.TabIndex = 5;
            this.textBoxnbneurcouche.Text = "6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "nb entrées";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "nb couches";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "nb neurones par couche";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(320, 263);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(297, 258);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(627, 106);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(287, 196);
            this.listBox1.TabIndex = 9;
            // 
            // textBoxnumneur
            // 
            this.textBoxnumneur.Location = new System.Drawing.Point(801, 33);
            this.textBoxnumneur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxnumneur.Name = "textBoxnumneur";
            this.textBoxnumneur.Size = new System.Drawing.Size(100, 22);
            this.textBoxnumneur.TabIndex = 10;
            this.textBoxnumneur.Text = "0";
            // 
            // textBoxnumcouche
            // 
            this.textBoxnumcouche.Location = new System.Drawing.Point(629, 33);
            this.textBoxnumcouche.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxnumcouche.Name = "textBoxnumcouche";
            this.textBoxnumcouche.Size = new System.Drawing.Size(100, 22);
            this.textBoxnumcouche.TabIndex = 11;
            this.textBoxnumcouche.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(627, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Numéro couche";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(797, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Numéro neurone";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(692, 70);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(169, 30);
            this.button3.TabIndex = 14;
            this.button3.Text = "Affiche info neurone";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBoxnbiter
            // 
            this.textBoxnbiter.Location = new System.Drawing.Point(93, 313);
            this.textBoxnbiter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxnbiter.Name = "textBoxnbiter";
            this.textBoxnbiter.Size = new System.Drawing.Size(79, 22);
            this.textBoxnbiter.TabIndex = 15;
            this.textBoxnbiter.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 287);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(276, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "nb d\'exemples pour chaque apprentissage";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 362);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(226, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "alpha (coefficient d\'apprentissage)";
            // 
            // textBoxalpha
            // 
            this.textBoxalpha.Location = new System.Drawing.Point(93, 380);
            this.textBoxalpha.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxalpha.Name = "textBoxalpha";
            this.textBoxalpha.Size = new System.Drawing.Size(55, 22);
            this.textBoxalpha.TabIndex = 18;
            this.textBoxalpha.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(303, 37);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(245, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "(les entrées comptent pour 1 couche)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(303, 66);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(139, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "(par couche cachée)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(303, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 17);
            this.label10.TabIndex = 21;
            this.label10.Text = "(y compris la constante)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(221, 545);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(399, 17);
            this.label11.TabIndex = 22;
            this.label11.Text = "En jaune la fonction à apprendre, en blanc la sortie du réseau";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(29, 170);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(210, 17);
            this.label12.TabIndex = 23;
            this.label12.Text = "Obligatoire pour créer le réseau";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(311, 164);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(242, 17);
            this.label13.TabIndex = 24;
            this.label13.Text = "Cliquez plusieurs fois pour converger";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 657);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxalpha);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxnbiter);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxnumcouche);
            this.Controls.Add(this.textBoxnumneur);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxnbneurcouche);
            this.Controls.Add(this.textBoxnbcouches);
            this.Controls.Add(this.textBoxnbentrees);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Perceptron multi-couches";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxnbentrees;
        private System.Windows.Forms.TextBox textBoxnbcouches;
        private System.Windows.Forms.TextBox textBoxnbneurcouche;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBoxnumneur;
        private System.Windows.Forms.TextBox textBoxnumcouche;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBoxnbiter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxalpha;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}