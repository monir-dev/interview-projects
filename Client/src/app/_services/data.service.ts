import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  baseUrl = "http://localhost:11819/api/";

  constructor(private http: HttpClient) { }

  fetchData = function() {
    this.http.get(this.baseUrl + "syllabus").subscribe((res: Response) => this.syllabuses = res);
  }

}
