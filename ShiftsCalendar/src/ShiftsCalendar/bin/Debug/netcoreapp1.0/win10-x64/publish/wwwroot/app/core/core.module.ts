import { NgModule, Optional, SkipSelf } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";

import { EntityService } from "./entity.service";
import { MessageService } from "./message.service";
import { throwIfAlreadyLoaded } from "./module-import-guard";
import { ExceptionService } from "./exception.service";
import { ToastModule } from "./toast/toast.module";
import { WaitCursorComponent } from "../shared/wait-cursor.component";
import { ShiftsDatelistComponent } from "../shifts/shifts-datelist.component";

// imports: imports the module's exports. which is usually declarables and providers
// in our case the spinner has no providers.
//
// exports: exports modules AND components/directives/pipes that other modules may want to use
@
NgModule({
    imports: [
        CommonModule, FormsModule, RouterModule,
        ToastModule
    ],
    exports: [
        CommonModule, FormsModule, RouterModule,
        ToastModule, WaitCursorComponent, ShiftsDatelistComponent
    ],
    providers: [
        EntityService,
        ExceptionService,
        MessageService
    ],
    declarations: [WaitCursorComponent, ShiftsDatelistComponent]
})
export class CoreModule {
    constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, "CoreModule");
    }
}
