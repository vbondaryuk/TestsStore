import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { IProject } from "../models/project";
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { IBuild } from "../models/build";

@Injectable({ providedIn: 'root' })
export class TestsStoreService {

    baseUrl = environment.baseUrl + "api/";

    constructor(private http: HttpClient) { }

    getProjects(): Observable<IProject[]> {
        return this.http.get<IProject[]>(this.baseUrl + "project/items")
            .pipe(
                tap(projects => console.log('fetched projects')),
                catchError(this.handleError('getProjects', []))
            );
    }

    getBuilds(projectId: string): Observable<IBuild[]> {
        return this.http.get<IBuild[]>(this.baseUrl + "build/project/"+projectId)
            .pipe(catchError(this.handleError('getBuilds', [])));
    }


    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
        };
    }
}