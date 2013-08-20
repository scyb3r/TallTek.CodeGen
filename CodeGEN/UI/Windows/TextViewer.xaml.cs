using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;

namespace CodeGEN.UI.Windows
{
    /// <summary>
    /// Interaction logic for TextViewer.xaml
    /// </summary>
    public partial class TextViewer : Window
    {
        public TextViewer()
        {
            InitializeComponent();
        }

        public TextViewer(string text, string syntaxDefinition = "SQL")
        {
            InitializeComponent();

            //load the SQL xshd from resource 
            IHighlightingDefinition customHighlighting;
            using (Stream s = typeof(TextViewer).Assembly.GetManifestResourceStream("CodeGEN.Resources.SQLSyntax.xshd"))
            {
                if (s == null)
                    throw new InvalidOperationException("Could not find embedded resource");
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
                        HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            // and register it in the HighlightingManager
            HighlightingManager.Instance.RegisterHighlighting("SQL", new string[] { ".sql" }, customHighlighting);
            txtEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition(syntaxDefinition);
            this.Text = text;
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;

                byte[] contentBytes = Encoding.UTF8.GetBytes(_text);
                MemoryStream ms = new MemoryStream(contentBytes);
                txtEditor.Load(ms);
            }

        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetDataObject(txtEditor.Text);
        }


    }
}
