export class Brand{
  Id
  Name

  constructor(brand) {
    if (brand === undefined){
      this.Id = 0;
      this.Name = "";
      return;
    }
    this.Id = brand.Id;
    this.Name = brand.Name;
  }
}

export class Shoe{
  Id
  Name
  Brand
  CreationTime
  PhotoFileName

  constructor(shoe) {
    if(shoe === undefined){
      this.Id = 0;
      this.Name = "";
      this.Brand = new Brand();
      this.CreationTime = Date.now();
      this.PhotoFileName = "anonimous.png"
    }
    else{
      this.Id = shoe.Id;
      this.Name = shoe.Name;
      this.Brand = shoe.Brand;
      this.CreationTime = shoe.CreationTime;
      this.PhotoFileName = shoe.PhotoFileName;
    }
  }
}
