#pragma checksum "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\Organizer\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fe480f880d644cc3c179e2f92003483c8215abd7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Organizer_Profile), @"mvc.1.0.view", @"/Views/Organizer/Profile.cshtml")]
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
#nullable restore
#line 1 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\_ViewImports.cshtml"
using EventApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\_ViewImports.cshtml"
using EventApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe480f880d644cc3c179e2f92003483c8215abd7", @"/Views/Organizer/Profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02fd785400d0433b687cc5ccdc5089fb098ca8f8", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Organizer_Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EventApp.DTOs.OrganizerDto>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\Organizer\Profile.cshtml"
  
    Layout = "_OrganizerDashBoard";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        FirstName:\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 13 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\Organizer\Profile.cshtml"
   Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        LastName:\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 19 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\Organizer\Profile.cshtml"
   Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        Email:\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 25 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\Organizer\Profile.cshtml"
   Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        Phone:\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 31 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\Organizer\Profile.cshtml"
   Write(Model.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        Organization:\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 37 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\Organizer\Profile.cshtml"
   Write(Model.Organization);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        Position:\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 43 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\Organizer\Profile.cshtml"
   Write(Model.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n\r\n    <dt class=\"col-sm-2\">\r\n        Address:\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 50 "C:\Users\BDUL LATEEF\Desktop\Abdullateef\Abdullateef File\Copy File\EventApp\Views\Organizer\Profile.cshtml"
   Write(Model.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n</dl> ");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EventApp.DTOs.OrganizerDto> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
