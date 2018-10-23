import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpEventType, HttpRequest} from '@angular/common/http';
import {environment} from '../../../../environments/environment';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  public progress: number;
  public message: string;

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
  }

  upload(files) {
    if (files.length === 0) {
      return;
    }

    const formData = new FormData();
    for (const file of files) {
      formData.append(file.Name, file);
    }

    formData.append('project', 'projectName');
    formData.append('build', 'buildName');

    const uri = environment.baseUrl + 'api/upload/trx';


    const uploadReq = new HttpRequest('POST', uri, formData, {reportProgress: true});
    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress) {
        this.progress = Math.round(100 * event.loaded / event.total);
      } else if (event.type === HttpEventType.Response) {
        this.message = 'Uploaded';
      }
    });
  }
}
