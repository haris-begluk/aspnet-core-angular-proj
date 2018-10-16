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
  query: any = {}; 
  columns = [
    {title:'Id'},
    {title:'Contact Name', key:'contactName', isSortable: true},
    {title:'Make', key:'make', isSortable: true},
    {title:'Model', key:'model', isSortable: true}, 
    {}
  ];
  
  constructor(private vehicleService: VehicleService) { }

  ngOnInit() { 
    this.vehicleService.getMakes() 
    .subscribe(makes => this.makes = makes);  
    this.vehicleService.getModels()
    .subscribe(models => this.allModels = models);
    this.populateVehicles();
  }  
  private populateVehicles(){
    this.vehicleService.getVehicles(this.query)
    .subscribe(vehicles => this.vehicles = vehicles);
  } 
  private populateModels(makeId){ 
    this.models = this.allModels.filter(m => m.makeId == this.query.makeId);    
    console.log(makeId);  
    console.log(this.models);
  } 
  onModelChange(){
    this.populateVehicles();
  }
  onFilterChange(){   
    this.populateModels(this.query.makeId); 
    this.populateVehicles();
  } 
  resetFilter(){
    this.query = {}; 
    this.onFilterChange();
  } 
  sortBy(columnName){
    if(this.query.sortBy === columnName){
      this.query.isSortAscending = !this.query.isSortAscending;
    }else{
      this.query.sortBy = columnName; 
      this.query.isSortAscending = true;
    } 
    this.populateVehicles();
  }
} 
