import { NgModule } from "@angular/core";
import { CoreModule } from "../core/core.module"; 

import { WorkerFormComponent } from "./worker-form.component";

import { WorkersRoutingModule, routedComponents } from "./workers-routing.module";

@NgModule({
    imports: [WorkersRoutingModule,
        CoreModule
    ],
    declarations: [
        
        WorkerFormComponent,
        routedComponents
    ]
})
export class WorkersModule { }

