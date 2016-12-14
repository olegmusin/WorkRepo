import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { ProjectsListComponent } from "./projects-list.component";
import { ProjectDetailComponent } from "./project-detail.component";


const routes: Routes = [
    {
        path: "",

        children: [
            {
                path: "",
                component: ProjectsListComponent
            },

            {
                path: ":id",
                component: ProjectDetailComponent
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProjectsRoutingModule { }

export const routedComponents = [ProjectsListComponent, ProjectDetailComponent];

