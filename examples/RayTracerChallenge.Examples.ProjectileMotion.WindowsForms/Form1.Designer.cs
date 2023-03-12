namespace RayTracerChallenge.Examples.ProjectileMotion.WindowsForms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        skControl1 = new SkiaSharp.Views.Desktop.SKControl();
        skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
        tableLayoutPanel1 = new TableLayoutPanel();
        tableLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // skControl1
        // 
        skControl1.Dock = DockStyle.Fill;
        skControl1.Location = new Point(78, 3);
        skControl1.Name = "skControl1";
        skControl1.Size = new Size(973, 399);
        skControl1.TabIndex = 1;
        skControl1.Text = "skControl1";
        skControl1.PaintSurface += skControl1_PaintSurface;
        // 
        // skglControl1
        // 
        skglControl1.BackColor = Color.Black;
        skglControl1.Dock = DockStyle.Fill;
        skglControl1.Location = new Point(79, 408);
        skglControl1.Margin = new Padding(4, 3, 4, 3);
        skglControl1.Name = "skglControl1";
        skglControl1.Size = new Size(971, 399);
        skglControl1.TabIndex = 2;
        skglControl1.VSync = true;
        skglControl1.PaintSurface += skglControl1_PaintSurface;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(skglControl1, 1, 1);
        tableLayoutPanel1.Controls.Add(skControl1, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(1054, 810);
        tableLayoutPanel1.TabIndex = 3;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1054, 810);
        Controls.Add(tableLayoutPanel1);
        Name = "Form1";
        Text = "Form1";
        tableLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private SkiaSharp.Views.Desktop.SKControl skControl1;
    private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
    private TableLayoutPanel tableLayoutPanel1;
}
