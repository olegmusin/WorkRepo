import { NgModule } from "@angular/core";


import { DashboardRoutingModule, routedComponents } from "./dashboard-routing.module";

@NgModule({
    imports: [DashboardRoutingModule],
    declarations: [routedComponents]
})
export class DashboardModule { }