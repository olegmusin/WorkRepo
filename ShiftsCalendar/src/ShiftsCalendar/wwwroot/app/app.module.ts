import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule } from "@angular/http";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";

import { PageNotFoundComponent } from "./page-not-found.component";
import { ProjectsService } from "./projects/projects.service";
import { WorkersService } from "./workers/workers.service";

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        AppRoutingModule
    ],
    declarations: [
        AppComponent,
        PageNotFoundComponent      
    ],
    providers: [ProjectsService, WorkersService],
    bootstrap: [AppComponent]
})
export class AppModule { }