import { Component, OnInit, Input } from "@angular/core";

@Component({
    moduleId: module.id,
    selector: "wait-cursor",
    templateUrl: "./wait-cursor.component.html"
})
export class WaitCursorComponent implements OnInit {

   
    isBusy: boolean;

    ngOnInit(): void {

        this.isBusy = true;
    }
      

}