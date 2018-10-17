import { ProgressService } from './../../services/progress.service';
import { Component, OnInit, ElementRef, ViewChild, NgZone } from '@angular/core';  
import { ToastrService } from 'ngx-toastr';
import { VehicleService } from './../../services/vehicle.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PhotoService } from '../../services/photo.service';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  
 
  @ViewChild('fileInput') fileInput:ElementRef;
  vehicle: any;
  vehicleId: number;  
  photos: any[]; 
  progress: any;
   constructor(  
    private zone: NgZone,
    private photoService:PhotoService,
    private route: ActivatedRoute, 
    private router: Router,
    private toastr: ToastrService,
    private vehicleService: VehicleService, 
    private progressService:ProgressService) { 
     route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/vehicles']);
        return; 
      }
    });
  }

  ngOnInit() {  
    this.photoService.getPhotos(this.vehicleId)
    .subscribe(photos => this.photos = photos);
    this.vehicleService.getVehicle(this.vehicleId)
    .subscribe(
      v => this.vehicle = v,
      err => {
        if (err.status == 404) {
          this.router.navigate(['/vehicles']);
          return; 
        }
      });
  }
  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    } 
  } 
  uploadPhoto(){
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement; 

     this.progressService.startTracking()
    .subscribe(progress => {
      // console.log(progress);
      this.zone.run(() => {
        this.progress = progress;
      });
    },
    null,
    () => { this.progress = null; });
    this.photoService.upload(nativeElement.files[0], this.vehicleId)
    .subscribe(photo => this.photos.push(photo));
  }
}
