import { Component, OnInit, Input, Output } from "@angular/core";
import { WorkersService } from "./workers.service";
import { Worker } from "./worker";

import "rxjs/Rx";

@Component({

    templateUrl: "app/workers/workers-list.component.html",
    styleUrls: ["/css/site.css"]

})
export class WorkersListComponent implements OnInit {
    isBusy: boolean;
    workers: Worker[] = [];
    errorMessage: string;

    constructor(private _workersService: WorkersService) { }

    ngOnInit(): void {
        this.isBusy = true;
        this._workersService.getWorkers()
            .subscribe(workers => this.workers = workers,
            error => this.errorMessage = error,
            () => this.isBusy = false);
    }

    addNewWorker(addedWorker: Worker): void {
        console.log(JSON.stringify(addedWorker));

        this.workers.push({
            id: addedWorker.id,
            name: addedWorker.name,
            specialty: addedWorker.specialty,
            salary: addedWorker.salary,
            shifts: addedWorker.shifts
        });
    }

    onDelete(worker): void {
        console.log(worker.id);
        this._workersService.deleteWorker(worker).subscribe(response => response);
        this.workers.splice(this.workers.indexOf(worker), 1);
    }
}