namespace Classification
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_caches = new System.Windows.Forms.TextBox();
            this.button_initialise = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_coeff = new System.Windows.Forms.TextBox();
            this.textBox_iterations = new System.Windows.Forms.TextBox();
            this.pictureBox_test = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_test)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de neurones cachés";
            // 
            // textBox_caches
            // 
            this.textBox_caches.Location = new System.Drawing.Point(189, 19);
            this.textBox_caches.Name = "textBox_caches";
            this.textBox_caches.Size = new System.Drawing.Size(75, 20);
            this.textBox_caches.TabIndex = 1;
            this.textBox_caches.Text = "3";
            // 
            // button_initialise
            // 
            this.button_initialise.Location = new System.Drawing.Point(32, 85);
            this.button_initialise.Name = "button_initialise";
            this.button_initialise.Size = new System.Drawing.Size(108, 52);
            this.button_initialise.TabIndex = 2;
            this.button_initialise.Text = "Lancer l\'apprentissage";
            this.button_initialise.UseVisualStyleBackColor = true;
            this.button_initialise.Click += new System.EventHandler(this.button_initialise_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Coeff apprentissage";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nombre itérations";
            // 
            // textBox_coeff
            // 
            this.textBox_coeff.Location = new System.Drawing.Point(140, 191);
            this.textBox_coeff.Name = "textBox_coeff";
            this.textBox_coeff.Size = new System.Drawing.Size(100, 20);
            this.textBox_coeff.TabIndex = 5;
            this.textBox_coeff.Text = "1";
            // 
            // textBox_iterations
            // 
            this.textBox_iterations.Location = new System.Drawing.Point(140, 220);
            this.textBox_iterations.Name = "textBox_iterations";
            this.textBox_iterations.Size = new System.Drawing.Size(100, 20);
            this.textBox_iterations.TabIndex = 6;
            this.textBox_iterations.Text = "100";
            // 
            // pictureBox_test
            // 
            this.pictureBox_test.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox_test.Image = global::Classification.Properties.Resources.img;
            this.pictureBox_test.Location = new System.Drawing.Point(270, 1);
            this.pictureBox_test.Name = "pictureBox_test";
            this.pictureBox_test.Size = new System.Drawing.Size(800, 800);
            this.pictureBox_test.TabIndex = 7;
            this.pictureBox_test.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 741);
            this.Controls.Add(this.pictureBox_test);
            this.Controls.Add(this.textBox_iterations);
            this.Controls.Add(this.textBox_coeff);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_initialise);
            this.Controls.Add(this.textBox_caches);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_test)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_caches;
        private System.Windows.Forms.Button button_initialise;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_coeff;
        private System.Windows.Forms.TextBox textBox_iterations;
        private System.Windows.Forms.PictureBox pictureBox_test;
    }
}

