import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
 
    rowData:any;
    columnDefs = [
        { headerName: 'Parking ID', field: 'parkingDisplayName', sortable: true, filter: true },
        { headerName: 'Building', field: 'locationBuilding', sortable: true, filter: true },
        { headerName: 'Locality', field: 'locationLocality', sortable: true, filter: true },
        { headerName: 'City', field: 'locationCity', sortable: true, filter: true },
        { headerName: 'Pin Code', field: 'locationPinCode', sortable: true, filter: true }
    ];
  
    //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    ////  this.rowData = this.http.get(baseUrl + 'api/Locations');
    //}  

    //ngOnInit() {
    //    this.rowData = this.http.get('https://api.myjson.com/bins/15psn9');
    //}
    onFirstDataRendered(params) {
        params.api.sizeColumnsToFit();
    }
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    }

    ngOnInit() {
        this.rowData = this.http.get(this.baseUrl +"api/ParkingLots");
    }
}

