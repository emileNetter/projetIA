namespace Classification3._2
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
            this.button_initialise = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox_coeff = new System.Windows.Forms.TextBox();
            this.label_coeff = new System.Windows.Forms.Label();
            this.button_algoKohonen = new System.Windows.Forms.Button();
            this.button_regroupement = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_initialise
            // 
            this.button_initialise.Location = new System.Drawing.Point(71, 25);
            this.button_initialise.Name = "button_initialise";
            this.button_initialise.Size = new System.Drawing.Size(93, 39);
            this.button_initialise.TabIndex = 0;
            this.button_initialise.Text = "Initialiser les données";
            this.button_initialise.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(316, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 800);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // textBox_coeff
            // 
            this.textBox_coeff.Location = new System.Drawing.Point(170, 187);
            this.textBox_coeff.Name = "textBox_coeff";
            this.textBox_coeff.Size = new System.Drawing.Size(100, 20);
            this.textBox_coeff.TabIndex = 2;
            this.textBox_coeff.Text = "0.1";
            // 
            // label_coeff
            // 
            this.label_coeff.AutoSize = true;
            this.label_coeff.Location = new System.Drawing.Point(63, 190);
            this.label_coeff.Name = "label_coeff";
            this.label_coeff.Size = new System.Drawing.Size(101, 13);
            this.label_coeff.TabIndex = 3;
            this.label_coeff.Text = "Coeff apprentissage";
            // 
            // button_algoKohonen
            // 
            this.button_algoKohonen.Location = new System.Drawing.Point(71, 70);
            this.button_algoKohonen.Name = "button_algoKohonen";
            this.button_algoKohonen.Size = new System.Drawing.Size(93, 44);
            this.button_algoKohonen.TabIndex = 4;
            this.button_algoKohonen.Text = "Algo Kohonen";
            this.button_algoKohonen.UseVisualStyleBackColor = true;
            // 
            // button_regroupement
            // 
            this.button_regroupement.Location = new System.Drawing.Point(71, 121);
            this.button_regroupement.Name = "button_regroupement";
            this.button_regroupement.Size = new System.Drawing.Size(93, 42);
            this.button_regroupement.TabIndex = 5;
            this.button_regroupement.Text = "Regroupement";
            this.button_regroupement.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 741);
            this.Controls.Add(this.button_regroupement);
            this.Controls.Add(this.button_algoKohonen);
            this.Controls.Add(this.label_coeff);
            this.Controls.Add(this.textBox_coeff);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_initialise);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_initialise;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox_coeff;
        private System.Windows.Forms.Label label_coeff;
        private System.Windows.Forms.Button button_algoKohonen;
        private System.Windows.Forms.Button button_regroupement;
    }
}

