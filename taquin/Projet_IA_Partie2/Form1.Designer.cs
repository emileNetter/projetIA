namespace Projet_IA_Partie2
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
            this.lancer_simulation = new System.Windows.Forms.Button();
            this.tb_nbErreurs = new System.Windows.Forms.TextBox();
            this.label_nbErreurs = new System.Windows.Forms.Label();
            this.label_poids1 = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.label_poids2 = new System.Windows.Forms.Label();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lancer_simulation
            // 
            this.lancer_simulation.Location = new System.Drawing.Point(211, 172);
            this.lancer_simulation.Name = "lancer_simulation";
            this.lancer_simulation.Size = new System.Drawing.Size(148, 50);
            this.lancer_simulation.TabIndex = 0;
            this.lancer_simulation.Text = "Lancer la simulation";
            this.lancer_simulation.UseVisualStyleBackColor = true;
            this.lancer_simulation.Click += new System.EventHandler(this.lancer_simulation_Click);
            // 
            // tb_nbErreurs
            // 
            this.tb_nbErreurs.Location = new System.Drawing.Point(297, 22);
            this.tb_nbErreurs.Name = "tb_nbErreurs";
            this.tb_nbErreurs.Size = new System.Drawing.Size(96, 20);
            this.tb_nbErreurs.TabIndex = 1;
            // 
            // label_nbErreurs
            // 
            this.label_nbErreurs.AutoSize = true;
            this.label_nbErreurs.Location = new System.Drawing.Point(204, 25);
            this.label_nbErreurs.Name = "label_nbErreurs";
            this.label_nbErreurs.Size = new System.Drawing.Size(87, 13);
            this.label_nbErreurs.TabIndex = 2;
            this.label_nbErreurs.Text = "Nombre d\'erreurs";
            // 
            // label_poids1
            // 
            this.label_poids1.AutoSize = true;
            this.label_poids1.Location = new System.Drawing.Point(208, 73);
            this.label_poids1.Name = "label_poids1";
            this.label_poids1.Size = new System.Drawing.Size(43, 13);
            this.label_poids1.TabIndex = 3;
            this.label_poids1.Text = "Poids X";
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(257, 70);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(93, 20);
            this.textBoxX.TabIndex = 4;
            // 
            // label_poids2
            // 
            this.label_poids2.AutoSize = true;
            this.label_poids2.Location = new System.Drawing.Point(208, 111);
            this.label_poids2.Name = "label_poids2";
            this.label_poids2.Size = new System.Drawing.Size(43, 13);
            this.label_poids2.TabIndex = 5;
            this.label_poids2.Text = "Poids Y";
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(257, 108);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(93, 20);
            this.textBoxY.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 532);
            this.Controls.Add(this.textBoxY);
            this.Controls.Add(this.label_poids2);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.label_poids1);
            this.Controls.Add(this.label_nbErreurs);
            this.Controls.Add(this.tb_nbErreurs);
            this.Controls.Add(this.lancer_simulation);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button lancer_simulation;
        private System.Windows.Forms.TextBox tb_nbErreurs;
        private System.Windows.Forms.Label label_nbErreurs;
        private System.Windows.Forms.Label label_poids1;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.Label label_poids2;
        private System.Windows.Forms.TextBox textBoxY;
    }
}

