import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpEventType, HttpRequest, HttpResponse} from '@angular/common/http';
import {Observable, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  baseUrl = environment.baseUrl + 'api/upload/';

  constructor(private http: HttpClient) {
  }

  public uploadTrx(files: Set<File>): { [key: string]: Observable<number> } {
    const status = {};
    files.forEach(file => {
      const formData: FormData = new FormData();
      formData.append(file.name, file);

      const url = this.baseUrl + 'trx';
      const req = new HttpRequest('POST', url, formData, {
        reportProgress: true
      });

      const progress = new Subject<number>();

      this.http.request(req)
        .subscribe(event => {
            if (event.type === HttpEventType.UploadProgress) {
              let percentageDone;
              if (!event.loaded || !event.total) {
                percentageDone = 0;
              } else {
                percentageDone = Math.round(100 * event.loaded / event.total);
              }
              progress.next(percentageDone);
            } else if (event instanceof HttpResponse) {
              progress.complete();
            }
          },
          (err) => {
            console.log('Upload Error:', err);
            progress.complete();
          });

      status[file.name] = {
        progress: progress.asObservable()
      };

    });

    return status;
  }
}
