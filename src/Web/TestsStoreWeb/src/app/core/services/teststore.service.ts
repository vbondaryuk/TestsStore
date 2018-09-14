import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable()
export class DataService {

    baseUrl = environment.baseUrl + "api/";

    constructor(private httpClient: HttpClient) { }
}