import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions} from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/do";
import "rxjs/add/operator/catch";
import "rxjs/Rx";
import { Worker } from "./worker";
import { Shift } from "../shifts/shift";


@Injectable()
export class WorkersService {

    constructor(private _http: Http) { }

    getWorkers(): Observable<Worker[]> {

        return (this._http.get(`api/workers/`)
            .map((response: Response) => <Worker[]>response.json())
            .do(data => console.log('All workers: ' + JSON.stringify(data)))
            .catch(this.handleError));
    };

  
    addWorker(newWorker: Worker): Observable<Worker> {

        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(newWorker);

        return (this._http.post(`api/workers/`, body, options)
            .map((response: Response) => <Worker>response.json())
            .do(data => console.log('New worker: ' + JSON.stringify(data)))
            .catch(this.handleError)) as any;
    };

    deleteWorker(worker: Worker): Observable<Response> {

        return (this._http.delete(`api/projects/delete/${worker.id}`)
            .map((response: Response) => response)
            .do(res => console.log('Deleted worker: ' + worker.name + " with status: " + JSON.stringify(res.statusText)))
            .catch(this.handleError));
    };

    getShiftsForWorker(worker: Worker): Observable<Shift[]> {

        return (this._http.get(`api/workers/${worker.id}/shifts`)
               .map((response: Response) => <Shift[]>response.json())
            .do(res => console.log('Shifts for worker: ' + worker.name + JSON.stringify(res)))
            .catch(this.handleError)
        );

    };

    getWorkerById(workerId: number): Observable<Worker> {

        return (this._http.get(`api/workers/${workerId}`)
            .map((response: Response) => <Worker>response.json())
            .do(res => console.log('Worker details: ' + JSON.stringify(res)))
            .catch(this.handleError)
        );
    };


    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || "Back-end error!");
    };


}