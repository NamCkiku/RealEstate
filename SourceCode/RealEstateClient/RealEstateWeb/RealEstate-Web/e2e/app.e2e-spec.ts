import { RealEstateWebPage } from './app.po';

describe('real-estate-web App', () => {
  let page: RealEstateWebPage;

  beforeEach(() => {
    page = new RealEstateWebPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
