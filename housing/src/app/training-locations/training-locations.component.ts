import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { TestServiceData } from 'src/app/services/static-test-data';
import { TrainingCenter } from 'src/interfaces/trainingcenter';

@Component({
  selector: 'dev-training-locations',
  templateUrl: './training-locations.component.html',
  styleUrls: ['./training-locations.component.scss']
})
export class TrainingLocationsComponent implements OnInit {
  // seeded Locations =>
  // import dummy TrainingCenter data
  public seededLocations: TrainingCenter[] = [
    TestServiceData.trainingcenter,
    TestServiceData.trainingcenter2,
    TestServiceData.trainingcenter,
    TestServiceData.trainingcenter2
  ];
  // id's for columns on material table
  displayedColumns = ['centerName', 'contactNumber', 'address'];
  // data source for material table
  dataSource = new MatTableDataSource<TrainingCenter>(this.seededLocations);

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor() { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
  }

}
