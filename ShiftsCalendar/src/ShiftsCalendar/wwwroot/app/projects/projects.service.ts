import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions} from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/do";
import "rxjs/add/operator/catch";
import "rxjs/Rx";
import { Project } from "./project";
import { Shift } from "../shifts/shift";


@Injectable()
export class ProjectsService {

    constructor(private _http: Http) { }

    getProjects(): Observable<Project[]> {

        return (this._http.get(`api/projects/`)
            .map((response: Response) => <Project[]>response.json())
            .do(data => console.log('All projects: ' + JSON.stringify(data)))
            .catch(this.handleError));
    };

  
    addProject(newProject: Project): Observable<Project> {

        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(newProject);

        return (this._http.post(`api/projects/`, body, options)
            .map((response: Response) => <Project>response.json())
            .do(data => console.log('New project: ' + JSON.stringify(data)))
            .catch(this.handleError)) as any;
    };

    deleteProject(project: Project): Observable<Response> {

        return (this._http.delete(`api/projects/delete/${project.id}`)
            .map((response: Response) => response)
            .do(res => console.log('Deleted project: ' + project.name + " with status: " + JSON.stringify(res.statusText)))
            .catch(this.handleError));
    };

    getShiftsForProject(project: Project): Observable<Shift[]> {

        return (this._http.get(`api/projects/${project.id}/shifts`)
               .map((response: Response) => <Shift[]>response.json())
            .do(res => console.log('Shifts for project: ' + project.name + "\n" + JSON.stringify(res)))
            .catch(this.handleError)
        );

    };

    getProjectById(projectId: number): Observable<Project> {

        return (this._http.get(`api/projects/${projectId}`)
            .map((response: Response) => <Project>response.json())
            .do(res => console.log('Project details: ' + JSON.stringify(res)))
            .catch(this.handleError)
        );
    };


    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || "Back-end error!");
    };


}