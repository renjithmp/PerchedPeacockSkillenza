import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
    title = 'app';
    rowData: any;
    constructor(private http: HttpClient) {

    }

    ngOnInit() {
        this.rowData = this.http.get('https://api.myjson.com/bins/15psn9');
    }
}
