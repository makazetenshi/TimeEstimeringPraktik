﻿#pragma checksum "..\..\Overview.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B3229E9BB5820FCD00F77BA027C1017F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
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


namespace praktik_estimering {
    
    
    /// <summary>
    /// Overview
    /// </summary>
    public partial class Overview : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Overview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonNewPeriod;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Overview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonViewOld;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Overview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonLogout;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Overview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagridOldPeriods;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/praktik estimering;component/overview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Overview.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\Overview.xaml"
            ((praktik_estimering.Overview)(target)).Loaded += new System.Windows.RoutedEventHandler(this.overviewLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.buttonNewPeriod = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\Overview.xaml"
            this.buttonNewPeriod.Click += new System.Windows.RoutedEventHandler(this.ClickNewPeriod);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonViewOld = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\Overview.xaml"
            this.buttonViewOld.Click += new System.Windows.RoutedEventHandler(this.clickViewOld);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buttonLogout = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\Overview.xaml"
            this.buttonLogout.Click += new System.Windows.RoutedEventHandler(this.clickLogout);
            
            #line default
            #line hidden
            return;
            case 5:
            this.datagridOldPeriods = ((System.Windows.Controls.DataGrid)(target));
            
            #line 18 "..\..\Overview.xaml"
            this.datagridOldPeriods.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.datagridOldPeriods_AutoGeneratingColumn);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 19 "..\..\Overview.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

