﻿#pragma checksum "..\..\..\ParaWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F0DECB2E199D57BD004AC219EBB29CF5B578A487"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp2;


namespace WpfApp2 {
    
    
    /// <summary>
    /// ParaWindow
    /// </summary>
    public partial class ParaWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MetierLabel;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonRetour;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonQuit;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LogMetier;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BoutonAjtMetier;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ExtensionsLabel;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox txt;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox pdf;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox png;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox jpg;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox docx;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\ParaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AjtExt;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EasySavev2;component/parawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ParaWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MetierLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.ButtonRetour = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\ParaWindow.xaml"
            this.ButtonRetour.Click += new System.Windows.RoutedEventHandler(this.Retour);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonQuit = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\ParaWindow.xaml"
            this.ButtonQuit.Click += new System.Windows.RoutedEventHandler(this.Button_Click2);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LogMetier = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 24 "..\..\..\ParaWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_France);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 34 "..\..\..\ParaWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Espagnol);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 39 "..\..\..\ParaWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_England);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BoutonAjtMetier = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\ParaWindow.xaml"
            this.BoutonAjtMetier.Click += new System.Windows.RoutedEventHandler(this.AjtMetier);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 45 "..\..\..\ParaWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_OuvreLM);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ExtensionsLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.txt = ((System.Windows.Controls.CheckBox)(target));
            
            #line 49 "..\..\..\ParaWindow.xaml"
            this.txt.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 12:
            this.pdf = ((System.Windows.Controls.CheckBox)(target));
            
            #line 50 "..\..\..\ParaWindow.xaml"
            this.pdf.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 13:
            this.png = ((System.Windows.Controls.CheckBox)(target));
            
            #line 51 "..\..\..\ParaWindow.xaml"
            this.png.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 14:
            this.jpg = ((System.Windows.Controls.CheckBox)(target));
            
            #line 52 "..\..\..\ParaWindow.xaml"
            this.jpg.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 15:
            this.docx = ((System.Windows.Controls.CheckBox)(target));
            
            #line 53 "..\..\..\ParaWindow.xaml"
            this.docx.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 16:
            this.AjtExt = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\ParaWindow.xaml"
            this.AjtExt.Click += new System.Windows.RoutedEventHandler(this.AjtExt_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

