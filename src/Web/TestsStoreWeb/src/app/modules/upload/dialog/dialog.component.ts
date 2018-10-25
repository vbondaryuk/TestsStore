import {Component, OnInit, ViewChild} from '@angular/core';
import {MatDialogRef} from '@angular/material';
import {UploadService} from '../../../core/services/upload.service';
import {forkJoin} from 'rxjs';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent {
  @ViewChild('file') file;
  public files: Set<File> = new Set<File>();

  progress;
  canBeClosed = true;
  primaryButtonText = 'Upload';
  showCancelButton = false;
  uploading = false;
  uploadSuccessful = false;

  constructor(public dialogRef: MatDialogRef<DialogComponent>, public uploadService: UploadService) {
  }

  addFiles() {
    this.file.nativeElement.click();
  }

  onFileAdded() {
    const files: { [file: string]: File } = this.file.nativeElement.files;
    for (const key in files) {
      if (!isNaN(Number(key))) {
        this.files.add(files[key]);
      }
    }
  }

  closeDialog() {
    if (this.uploadSuccessful) {
      return this.dialogRef.close();
    }

    this.uploading = true;
    this.progress = this.uploadService.uploadTrx(this.files);

    const allProgressObservable = [];

    for (const key in this.progress) {
      if (this.progress.hasOwnProperty(key)) {
        allProgressObservable.push(this.progress[key].progress);
      }
    }

    this.primaryButtonText = 'Finish';
    this.canBeClosed = false;
    this.dialogRef.disableClose = true;
    this.showCancelButton = false;

    forkJoin(allProgressObservable).subscribe(() => {
      this.canBeClosed = true;
      this.dialogRef.disableClose = false;
      this.uploadSuccessful = true;
      this.uploading = false;
    });
  }
}
