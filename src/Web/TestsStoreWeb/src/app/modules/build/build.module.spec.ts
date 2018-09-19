import { BuildModule } from './build.module';

describe('BuildModule', () => {
  let buildModule: BuildModule;

  beforeEach(() => {
    buildModule = new BuildModule();
  });

  it('should create an instance', () => {
    expect(buildModule).toBeTruthy();
  });
});
