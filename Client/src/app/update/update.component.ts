import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  syllabusUploaderName = "Upload Syllabus";
  testPlanUploaderName = "Upload Test Plan";

  item: any = {};
  errors: any = {};
  trades: any = [];
  levels: any = [];
  uploadedSyllabus = '';
  uploadedTestPlan = '';

  baseUrl = "http://localhost:11819/api/";

  constructor(private http: HttpClient, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');

    this.http.get(this.baseUrl + 'syllabus/' + id).subscribe((res: Response) => {
      this.item = res;
      this.http.get(this.baseUrl + 'syllabus/levels/' + this.item.tradeId).subscribe((res: Response) => this.levels = res);
    });

    this.http.get(this.baseUrl + 'syllabus/trades').subscribe((res: Response) => this.trades = res);

  }

  public syllabusUploadFinished = (event) => {
      this.uploadedSyllabus = event.dbPath;
  }

  public testPlanUploadFinished = (event) => {
    this.uploadedTestPlan = event.dbPath;
  }

  upadateItem() {
    this.errors = {};
    this.item.uploadedSyllabus = this.uploadedSyllabus;
    this.item.uploadedTestPlan = this.uploadedTestPlan;

    console.log(this.item);

    this.http.put(this.baseUrl + 'syllabus/' + this.item.id, this.item).subscribe( next => {
      this.alertify.success("successfull");
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

