﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrokenGrenade.Web.App.Resources.Areas.Identity.Pages.Account {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResetPasswordResource {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResetPasswordResource() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("BrokenGrenade.Web.App.Resources.Areas.Identity.Pages.Account.ResetPasswordResourc" +
                            "e", typeof(ResetPasswordResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string ResetPasswordTitle {
            get {
                return ResourceManager.GetString("ResetPasswordTitle", resourceCulture);
            }
        }
        
        public static string ResetPasswordButton {
            get {
                return ResourceManager.GetString("ResetPasswordButton", resourceCulture);
            }
        }
        
        public static string ResetPasswordConfirmation {
            get {
                return ResourceManager.GetString("ResetPasswordConfirmation", resourceCulture);
            }
        }
        
        public static string PasswordsDontMatch {
            get {
                return ResourceManager.GetString("PasswordsDontMatch", resourceCulture);
            }
        }
        
        public static string ResetCodeMissing {
            get {
                return ResourceManager.GetString("ResetCodeMissing", resourceCulture);
            }
        }
        
        public static string UserIdMissing {
            get {
                return ResourceManager.GetString("UserIdMissing", resourceCulture);
            }
        }
    }
}
