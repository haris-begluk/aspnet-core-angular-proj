import { AppPage } from './../../../../e2e/app.po';
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
  query: any = { 
    pageSize :3
  }; 
  columns = [
    {title:'Id'},
    {title:'Contact Name', key:'contactName', isSortable: true},
    {title:'Make', key:'make', isSortable: true},
    {title:'Model', key:'model', isSortable: true}, 
    {}
  ];
  queryResult: any = {};
  
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
    .subscribe(result => this.queryResult = result);
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
    this.query.page = 1; 
    //this.query.pageSize = this.PAGE_SIZE;
    this.populateModels(this.query.makeId); 
    this.populateVehicles();
  } 
  resetFilter(){
    this.query = { 
      page:1, 
      pageSize: 3
    }; 
    this.populateVehicles();
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
  onPageChange(page){
    this.query.page = page; 
    this.populateVehicles();
  }
} 
