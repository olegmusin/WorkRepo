import { NgModule } from "@angular/core";
import { CoreModule } from "../core/core.module";

import { ProjectFormComponent } from "./project-form.component";

import { ProjectsRoutingModule, routedComponents } from "./projects-routing.module";

@NgModule({
    imports: [ProjectsRoutingModule,
        CoreModule
    ],
    declarations: [

        ProjectFormComponent,
        routedComponents
    ]
})
export class ProjectsModule { }

