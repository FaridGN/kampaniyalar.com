#pragma checksum "C:\Users\User\Desktop\projects\kampaniyalar\Foroffer\Views\Admin\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7cf19a580c7910c0bb1fcafd5fcf1d6a47442fc5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Edit), @"mvc.1.0.view", @"/Views/Admin/Edit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Edit.cshtml", typeof(AspNetCore.Views_Admin_Edit))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\User\Desktop\projects\kampaniyalar\Foroffer\Views\_ViewImports.cshtml"
using Foroffer;

#line default
#line hidden
#line 2 "C:\Users\User\Desktop\projects\kampaniyalar\Foroffer\Views\_ViewImports.cshtml"
using Foroffer.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7cf19a580c7910c0bb1fcafd5fcf1d6a47442fc5", @"/Views/Admin/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"011a5aeebccbb42fe3a68e32f88ac0fa3b7716be", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\User\Desktop\projects\kampaniyalar\Foroffer\Views\Admin\Edit.cshtml"
  
    Layout = "AdminLayout";

#line default
#line hidden
            BeginContext(38, 321, true);
            WriteLiteral(@"
        <section class=""buttonpanel"">
            <h5 style=""font-weight:bold"">Edit Post</h5>
        </section>
        <section class=""posts"">
            <div class=""post-container"">
                <div class=""row"">
                    <div class=""col-lg-3 col-md-4 col-sm-6 col-12"">
                        ");
            EndContext();
            BeginContext(359, 1439, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "754b3607e126414c9f232e108f02c2cd", async() => {
                BeginContext(365, 1126, true);
                WriteLiteral(@"
                            <label for=""title"" style=""width: 45px"">Title</label>
                            <input class=""postinput"" type=""text""><br>
                            <label for=""description"" style=""width: 45px"">Description</label><br>
                            <textarea class=""texbox"" type=""text""></textarea><br>
                            <label for=""file"">Upload Image</label>
                            <input class=""forupload"" type=""file"" name=""file""><br>
                            <label for=""date"" style=""width:80px"">Start Date</label>
                            <input class=""postinput"" type=""date"" name=""date""><br>
                            <label for=""enddate"" style=""width: 80px"">End Date</label>
                            <input class=""postinput"" type=""date"" name=""date""><br>
                            <label for=""more"" style=""width:45px"">URL</label>
                            <input class=""postinput"" type=""text"" placeholder=""URL""><br>
                            <labe");
                WriteLiteral("l for=\"select\">Company</label>\r\n                            <select>\r\n                                ");
                EndContext();
                BeginContext(1491, 32, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fb0126685db44c7185a776be5ca4323c", async() => {
                    BeginContext(1499, 15, true);
                    WriteLiteral("Auto Azerbaijan");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1523, 34, true);
                WriteLiteral("\r\n                                ");
                EndContext();
                BeginContext(1557, 27, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2af80a21f2894ab29c54f2b29acdf62a", async() => {
                    BeginContext(1565, 10, true);
                    WriteLiteral("Kia Motors");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1584, 34, true);
                WriteLiteral("\r\n                                ");
                EndContext();
                BeginContext(1618, 26, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d93ec14ff7ab48c0880d65b03dae8fb8", async() => {
                    BeginContext(1626, 9, true);
                    WriteLiteral("Any other");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1644, 147, true);
                WriteLiteral("\r\n                            </select><br>\r\n                            <button class=\"push\" type=\"submit\">Edit</button>\r\n                        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1798, 402, true);
            WriteLiteral(@"

                    </div>
                    <div class=""col-lg-3 col-md-4 col-sm-6 col-12"">

                    </div>
                    <div class=""col-lg-3 col-md-4 col-sm-6 col-12"">

                    </div>
                    <div class=""col-lg-3 col-md-4 col-sm-6 col-12"">

                    </div>
                </div>
            </div>
        </section>

       ");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
