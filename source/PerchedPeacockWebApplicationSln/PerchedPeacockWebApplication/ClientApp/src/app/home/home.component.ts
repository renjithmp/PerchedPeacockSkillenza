import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {


    rowData: any;
    booking: any;
    
    columnDefs = [
        {headerName: 'ID', field: 'id', sortable: true, filter: true },
        { headerName: 'Parking ID', field: 'parkingDisplayName', sortable: true, filter: true },
        { headerName: 'Building', field: 'locationBuilding', sortable: true, filter: true },
        { headerName: 'Locality', field: 'locationLocality', sortable: true, filter: true },
        { headerName: 'City', field: 'locationCity', sortable: true, filter: true },
        { headerName: 'Pin Code', field: 'locationPinCode', sortable: true, filter: true },
        { headerName: "Hourly Rate", field: "hourlyRate", width: 90, cellRenderer: this.hourlyCharge },
          { headerName: "Book Me", field: "bookALot", width: 90, cellRenderer: this.bookingCellRendererFunc }
    ];

    bookingGridOptions: {

    }
    openSnackBar = function (message: string, action: string) {
        this._snackBar.open(message, action, {
            duration: 2000,
        })
    }
    onBookingCellClicked=function($event){
        console.log($event);
        this.postBooking($event).subscribe(() => {
            this.rowData = this.http.get(this.baseUrl + "api/Bookings/AllFreeParkingSlots");
            this.openSnackBar("Booking Done !!", "Well Done");
        }) 
        
    }
    postBooking($event): Observable<any> {

        return this.http.post(this.baseUrl + "api/Bookings?parkingLotId=" + $event.data['id'], null);

    }

    bookingCellRendererFunc(params) {
   // params.$scope.ageClicked = this.ageClicked;
    //return '<button ng-click="ageClicked(data.age)" ng-bind="data.age"></button>';
    return '<button >Book Me</button>';
    }

    hourlyCharge(params) {
        // params.$scope.ageClicked = this.ageClicked;
        //return '<button ng-click="ageClicked(data.age)" ng-bind="data.age"></button>';
        return '<div >RS:50.00 </div>';
    }

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private _snackBar: MatSnackBar) {

    }

    ngOnInit() {
        this.rowData = this.http.get(this.baseUrl + "api/Bookings/AllFreeParkingSlots");
    }
}
