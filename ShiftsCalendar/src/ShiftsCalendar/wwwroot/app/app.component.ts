import { Component } from "@angular/core";

declare var $: JQueryStatic;

@Component({
        moduleId: module.id,
        selector: "sc-app",
        templateUrl: "app.component.html",
        styleUrls: ["/css/site.css"]
    })

export class AppComponent {
    pageTitle: string = "Shifts Calendar";
    
    togglePanel(): void {

        var $sidebarAndWrapper = $("#sidebar, #wrapper");
        var $icon = $("#sidebarToggle i.fa");

        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-double-left");
            $icon.addClass("fa-angle-double-right");
        }
        else {
            $icon.addClass("fa-angle-double-left");
            $icon.removeClass("fa-angle-double-right");
        }


    };

}