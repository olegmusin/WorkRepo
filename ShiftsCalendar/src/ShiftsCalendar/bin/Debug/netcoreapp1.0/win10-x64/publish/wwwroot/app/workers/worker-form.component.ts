import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Worker } from "./worker";
import { WorkersService } from "./workers.service";


@Component({
    moduleId: module.id,
    selector: "worker-form",
    templateUrl: "./worker-form.component.html",
    styleUrls: ["/css/site.css"]
})
export class WorkerFormComponent implements OnInit {

    @Input()
    newWorker: Worker;
    errorMessage: string;
    @Output()
    wrkAdded: EventEmitter<Worker> = new EventEmitter<Worker>();
    constructor(private _workersService: WorkersService) { }

    ngOnInit(): void {

        this.newWorker = new Worker();
       
    }

    onSubmit(workerCreated) {

        this._workersService.addWorker(workerCreated.value)
            .subscribe(response => this.newWorker = response,
                        error => this.errorMessage = <any>error,
                        () => {
                            this.wrkAdded.emit(this.newWorker);
                            workerCreated.reset();
            });
    }
}