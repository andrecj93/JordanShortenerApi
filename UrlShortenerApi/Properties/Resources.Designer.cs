﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UrlShortenerApi.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("UrlShortenerApi.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UON - URL Shortener.
        /// </summary>
        public static string AppDefaultName {
            get {
                return ResourceManager.GetString("AppDefaultName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AppName.
        /// </summary>
        public static string AppName_Key {
            get {
                return ResourceManager.GetString("AppName_Key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BearerAuthTokenExpireInMinutes.
        /// </summary>
        public static string BearerAuthTokenExpireInMinutes_Key {
            get {
                return ResourceManager.GetString("BearerAuthTokenExpireInMinutes_Key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TokenGenerator_ExpireDays.
        /// </summary>
        public static string TokenGenerator_ExpireDays_key {
            get {
                return ResourceManager.GetString("TokenGenerator_ExpireDays_key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TokenGenerator_MaxLength.
        /// </summary>
        public static string TokenGenerator_MaxLength_Key {
            get {
                return ResourceManager.GetString("TokenGenerator_MaxLength_Key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TokenGenerator_MinLength.
        /// </summary>
        public static string TokenGenerator_MinLength_Key {
            get {
                return ResourceManager.GetString("TokenGenerator_MinLength_Key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Link Inválido!.
        /// </summary>
        public static string TokenInvalid {
            get {
                return ResourceManager.GetString("TokenInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Link Expirado!.
        /// </summary>
        public static string TokenNotActive {
            get {
                return ResourceManager.GetString("TokenNotActive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Link Inexistente!.
        /// </summary>
        public static string TokenNotFound {
            get {
                return ResourceManager.GetString("TokenNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocorreu um erro desconhecido. Por favor, contacte um administrador!.
        /// </summary>
        public static string UnknownError {
            get {
                return ResourceManager.GetString("UnknownError", resourceCulture);
            }
        }
    }
}