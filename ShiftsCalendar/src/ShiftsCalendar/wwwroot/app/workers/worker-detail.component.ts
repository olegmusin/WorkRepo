import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from '@angular/router';
import { Worker } from "./worker";
import { WorkersService } from "./workers.service";

@Component({
    moduleId: module.id,
  
    templateUrl: "worker-detail.component.html",
    styleUrls: ["/css/site.css"]
})

export class WorkerDetailComponent implements OnInit {


    worker: Worker;
    pageTitle = "Worker Details";
    constructor(private _workersService: WorkersService, private _route: ActivatedRoute) { }

    ngOnInit(): void {

      let id = this._route.snapshot.params['id']; 

      this._workersService.getWorkerById(id)
            .subscribe(res => { this.worker = res; });
    }
}