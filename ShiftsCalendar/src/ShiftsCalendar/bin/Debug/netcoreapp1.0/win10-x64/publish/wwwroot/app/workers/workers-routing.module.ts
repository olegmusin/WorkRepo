import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { WorkersListComponent } from "./workers-list.component";
import { WorkerDetailComponent } from "./worker-detail.component";


const routes: Routes = [
    {
        path: "",

        children: [
            {
                path: "",
                component: WorkersListComponent
            },
            {
                path: ":id",
                component: WorkerDetailComponent
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class WorkersRoutingModule { }

export const routedComponents = [WorkersListComponent, WorkerDetailComponent];

