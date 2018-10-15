import { KeyValuePair } from './../../models/KeyValuePair';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../../models/vehicle';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles: Vehicle[];  
  makes: KeyValuePair[];  
  models: any[]; 
  allModels:any[];
  filter: any = {};
  
  constructor(private vehicleService: VehicleService) { }

  ngOnInit() { 
    this.vehicleService.getMakes() 
    .subscribe(makes => this.makes = makes);  
    this.vehicleService.getModels()
    .subscribe(models => this.allModels = models);
    this.populateVehicles();
  }  
  private populateVehicles(){
    this.vehicleService.getVehicles(this.filter)
    .subscribe(vehicles => this.vehicles = vehicles);
  } 
  private populateModels(makeId){ 
    this.models = this.allModels.filter(m => m.makeId == this.filter.makeId);    
    console.log(makeId);  
    console.log(this.models);
  } 
  onModelChange(){
    this.populateVehicles();
  }
  onFilterChange(){   
    this.populateModels(this.filter.makeId); 
    this.populateVehicles();
  } 
  resetFilter(){
    this.filter = {}; 
    this.onFilterChange();
  }
} 
