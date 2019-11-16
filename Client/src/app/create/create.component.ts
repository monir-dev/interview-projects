import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  syllabusUploaderName = "Upload Syllabus";
  testPlanUploaderName = "Upload Test Plan";

  item: any = {};
  errors: any = {};
  trades: any = [];
  levels: any = [];
  uploadedSyllabus = '';
  uploadedTestPlan = '';

  baseUrl = "http://localhost:11819/api/";

  constructor(private http: HttpClient, private alertify: AlertifyService) { }

  ngOnInit() {
    this.http.get(this.baseUrl + 'syllabus/trades').subscribe((res: Response) => {
      this.trades = res;
    });

  }

  public syllabusUploadFinished = (event) => {
      this.uploadedSyllabus = event.dbPath;
  }

  public testPlanUploadFinished = (event) => {
    this.uploadedTestPlan = event.dbPath;
  }

  addNewItem() {
    this.errors = {};
    this.item.uploadedSyllabus = this.uploadedSyllabus;
    this.item.uploadedTestPlan = this.uploadedTestPlan;

    this.http.post(this.baseUrl + 'syllabus', this.item).subscribe( next => {
      this.alertify.success("successfull");
      this.item = {};
    }, (error: HttpErrorResponse) => {
      this.errors = error.error.errors;
    });
  }

  tradeOnChange($event) {
    const tradeId = $event.target.value;
    if(tradeId === 0) {
      this.levels = [];
    } else {
      this.http.get(this.baseUrl + 'syllabus/levels/' + tradeId).subscribe((res: Response) => {
        this.levels = res;
      });
    }
  }


  clear() {
    this.errors = {};
    this.item = {};
  }
}

