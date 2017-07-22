'use strict';

/**
 * @ngdoc function
 * @name angularDemoApp.controller:ViewerCtrl
 * @description
 * # ViewerCtrl
 * Controller of the angularDemoApp
 */

(function (app) {
    app.controller('viewerController', viewerController);

    viewerController.$inject = ['$scope', '$modal', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'ENUMS', 'blockUI'];

    function viewerController($scope, $modal, BaseService, apiService, $rootScope, $window, $timeout, ENUMS, blockUI) {
        $scope.init = function () {
            // Set full screen mode for the viewer
            var options = new Stimulsoft.Viewer.StiViewerOptions();
            options.appearance.scrollbarsMode = true;
            var viewer = new $window.Stimulsoft.Viewer.StiViewer(options, 'StiViewer', false);
            var report = new $window.Stimulsoft.Report.StiReport();
            report.loadFile($rootScope.baseUrl + 'ReportTemplates/Report');
            viewer.report = report;
            // Create new DataSet object
            var dataSet = new Stimulsoft.System.Data.DataSet("Demo");
            // Load JSON data file from specified URL to the DataSet object
            dataSet.readJsonFile($rootScope.baseUrl + 'ReportTemplates/Demo.json');
            // Remove all connections from the report template
            report.dictionary.databases.clear();
            // Register DataSet object
            report.regData("Demo", "Demo", dataSet);
            // Render report with registered data
            report.render();
            viewer.renderHtml('viewer');
        };
        //$scope.init();

        var viewer = new Stimulsoft.Viewer.StiViewer(null, "StiViewer", false);

        $scope.GetAllProvince = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            apiService.post('Management/LoadAllWard', true, null, function (respone) {
                if (respone.data.success == true) {
                    var jsonStrings = respone.data.lstData;
                    var jsonData = JSON.stringify(jsonStrings);
                    var dataSet = new Stimulsoft.System.Data.DataSet();

                    dataSet.readJson(jsonData);
                    var data = dataSet.tables.getByIndex(0);
                    var report = new Stimulsoft.Report.StiReport();
                    //Add data to datastore
                    report.regData("data", "data", dataSet);
                    //Fill dictionary
                    var dataSource = new Stimulsoft.Report.Dictionary.StiDataTableSource(data.tableName, data.tableName, data.tableName);
                    dataSource.columns.add(new Stimulsoft.Report.Dictionary.StiDataColumn("Id", "Id", "Id"));
                    dataSource.columns.add(new Stimulsoft.Report.Dictionary.StiDataColumn("Name", "Name", "Name"));
                    dataSource.columns.add(new Stimulsoft.Report.Dictionary.StiDataColumn("DistrictID", "DistrictID", "DistrictID"));
                    report.dictionary.dataSources.add(dataSource);

                    var page = report.pages.getByIndex(0);

                    //Create HeaderBand
                    var headerBand = new Stimulsoft.Report.Components.StiHeaderBand();
                    headerBand.height = 0.5;
                    headerBand.name = "HeaderBand";
                    page.components.add(headerBand);

                    //Create Databand
                    var dataBand = new Stimulsoft.Report.Components.StiDataBand();
                    dataBand.dataSourceName = data.tableName;
                    dataBand.height = 0.5;
                    dataBand.name = "DataBand";
                    page.components.add(dataBand);

                    //Create texts
                    var pos = 0;


                    var columnWidth = Stimulsoft.Base.StiAlignValue.alignToMinGrid(page.width / data.columns.count, 0.1, true);

                    var nameIndex = 15;
                    for (var index in data.columns.list) {
                        var dataColumn = data.columns.list[index];
                        //console.log(dataColumn);
                        //Create text on header
                        var options = new Stimulsoft.Designer.StiDesignerOptions();
                        options.appearance.fullScreenMode = false;
                        var designer = new Stimulsoft.Designer.StiDesigner(options, 'StiDesigner', false);
                        var headerText = new Stimulsoft.Report.Components.StiText();
                        headerText.clientRectangle = new Stimulsoft.Base.Drawing.RectangleD(pos, 0, columnWidth, 0.5);
                        headerText.text = dataColumn.caption;
                        headerText.horAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
                        headerText.name = "HeaderText" + nameIndex.toString() + 'Header';
                        headerText.brush = new Stimulsoft.Base.Drawing.StiSolidBrush(Stimulsoft.System.Drawing.Color.lightpurple);
                        headerText.border.side = Stimulsoft.Base.Drawing.StiBorderSides.All;
                        headerBand.components.add(headerText);
                        //   "Watermark":{"TextBrush":"solid:50,0,0,0","Text":"LabourExpress"},

                        //Create text on Data Band
                        var dataText = new Stimulsoft.Report.Components.StiText();
                        dataText.clientRectangle = new Stimulsoft.Base.Drawing.RectangleD(pos, 0, columnWidth, 0.5);
                        //dataText.text = format("{{0}.{1}}", data.tableName, dataColumn.columnName);
                        String.format = function () {
                            var s = arguments[0];
                            for (var i = 0; i < arguments.length - 1; i++) {
                                var reg = new RegExp("\\{" + i + "\\}", "gm");
                                s = s.replace(reg, arguments[i + 1]);
                            }
                            return s;
                        };
                        dataText.text = String.format("{{0}.{1}}", data.tableName, dataColumn.columnName);

                        dataText.name = "DataText" + nameIndex.toString();
                        dataText.border.side = Stimulsoft.Base.Drawing.StiBorderSides.All;

                        //Add highlight
                        var condition = new Stimulsoft.Report.Components.StiCondition();
                        condition.backColor = Stimulsoft.System.Drawing.Color.gray;
                        condition.textColor = Stimulsoft.System.Drawing.Color.black;
                        condition.expression = "(Line & 1) == 1";
                        condition.item = Stimulsoft.Report.Components.StiFilterItem.Expression;
                        dataText.conditions.add(condition);

                        dataBand.components.add(dataText);

                        pos = pos + columnWidth;

                        nameIndex++;
                    }

                    //Create FooterBand
                    var footerBand = new Stimulsoft.Report.Components.StiFooterBand();
                    footerBand.height = 0.5;
                    footerBand.name = "FooterBand";
                    page.components.add(footerBand);

                    //Create text on footer
                    var footerText = new Stimulsoft.Report.Components.StiText();
                    footerText.clientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, page.width, 0.5);
                    footerText.text = "Count - {Count()}";
                    footerText.horAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
                    footerText.name = "FooterText";
                    footerText.brush = new Stimulsoft.Base.Drawing.StiSolidBrush(Stimulsoft.System.Drawing.Color.white);
                    footerBand.components.add(footerText);



                    var Watermark = new Stimulsoft.Report.Components.StiText();
                    Watermark.clientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, page.width, 0.5);
                    Watermark.text = "Count - {Count()}";
                    Watermark.brush = new Stimulsoft.Base.Drawing.StiSolidBrush(Stimulsoft.System.Drawing.Color.black);
                    viewer.report = report;

                    viewer.renderHtml('viewer');
                    myBlockUI.stop();
                } else {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu Tỉnh Thành", 3000);
                }
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu Tỉnh Thành", 3000);
            });
        }
        $scope.GetAllProvince();
    }
})(angular.module('myApp'));