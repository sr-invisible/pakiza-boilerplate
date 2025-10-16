
        var IpbLoadingBusy = false;
        //......... SingleDoubleClick
        jQuery.fn.IpbTagNameSingleDoubleClick = function (single_click_callback, double_click_callback, timeout) {
            return this.each(function () {
                var clicks = 0, self = this;
                jQuery(this).click(function (event) {
                    clicks++;
                    if (clicks == 1) {
                        setTimeout(function () {
                            if (clicks == 1) {
                                single_click_callback.call(self, event);
                            } else {
                                double_click_callback.call(self, event);
                            }
                            clicks = 0;
                        }, timeout || 300);
                    }
                });
            });
        }
        $(".IpbTagName").IpbTagNameSingleDoubleClick(function () {
            var _this = $(this), hClass = _this.hasClass("IpbTagSel");
            $('.IpbTagName').removeClass('IpbTagSel');
            $('.IpbModuleSubDetl').text('');
            if (!hClass) {
                _this.closest('div').find('.IpbModuleSubDetl').text(_this.data('ipbdetl'));
                _this.addClass('IpbTagSel');
            }
        }, function () { //.......... dblclick  
            var _this = $(this),
                IsSmall = _this.hasClass("IpbTagSmall"),
                PCode = _this.attr('id').match(/\d+/).toString(),
                ModName = $('#ModuleName').val(),
                UserId = $('#UserId').val(),
                AreaId = $('#AreaId').val();

            if (IpbLoadingBusy) UIkit.notify("Please Wait.....", { pos: 'top-right', status: 'warning' });
            else if (!PCode) UIkit.notify("Sorry, Check The Entity!!", { pos: 'top-right', status: 'warning' });
            else if ($('#IpbChart' + PCode).length > 0) UIkit.notify("Sorry, Entity Already On The Panel!!", { pos: 'top-right', status: 'warning' });
            else {
                IpbLoadingBusy = true;
                $.post("/ADM/iPanel/IpbChartFunction_jFindAll", { PCode: PCode, ModName: ModName, UserId: UserId, AreaId: AreaId }, function (data) {
                    if (data[0].ChartData.length > 0) {
                        if (_this.hasClass("IpbTagSel")) $('.IpbModuleSubDetl').text('');
                        _this.removeClass('IpbTagSel').addClass('IpbHideThis');
                        IsSmall ? $('#IpbChartSmallArea').append(data[0].ChartData) : $('#IpbChartFullArea').append(data[0].ChartData);
                        IpbLoadingBusy = false;
                        IpbFnInitialization(PCode);
                    }
                    else IpbLoadingBusy = false;
                });
            }
        });

        //.........CloseBtn
        $("body").on("click", ".IpbCloseBtn", function (event) {
            var _this = $(this),
                _Entity = $(_this).closest('.IpbChartEntity'),
                ipbcode = _Entity.attr('id').match(/\d+/);
            $('#IpbTagName' + ipbcode).removeClass('IpbHideThis');
            _Entity.remove();
        });
		
        //.........SaveBtn
        $("body").on("click", "#IpbSaveBtn", function (event) {
            var UserId = $('#UserId').val(),
                ModName = $('#ModuleName').val(),
                PCode = $(".IpbChartEntity").map(function () {
                    return $(this).attr("id").match(/\d+/);
                }).get().join(",");

            if (IpbLoadingBusy) UIkit.notify("Please Wait.....", { pos: 'top-right', status: 'warning' });
            else {
                UIkit.modal.confirm('Are you sure to Save this?', function () {
                    IpbLoadingBusy = true;
                    $.post("/ADM/iPanel/IPanelView_Save", { UserId: UserId, ModName: ModName, PCode: PCode }, function (data) {
                        UIkit.notify(data.Message, { pos: 'top-right', status: data.Success ? 'success' : 'danger' });
                        IpbLoadingBusy = false;
                    });
                }, { labels: { 'Ok': 'Yes', 'Cancel': 'No'} });
            }
        });
		
		
		//..... Chart Functions
        function IpbFnFirstBooting() {
            var PCode = $(".IpbChartEntity").map(function () {
                return $(this).attr("id").match(/\d+/);
            }).get().join(",");
            if (PCode.length > 0) IpbFnInitialization(PCode);
        }
        function IpbFnInitialization(PCode) {
            //...................... Chart Functions
            function HC_PieDDown(_code) {
                var chId = 'IpbChart_chId' + _code,
                    chTitle = $('#IpbChart_chTitle' + _code).text(),
                    chSubTitle = $('#IpbChart_chSubTitle' + _code).text(),
                    chSeries = $('#IpbChart_chSeries' + _code).text(),
                    chDDown = $('#IpbChart_chDDown' + _code).text();

                if ($('#' + chId).length) {
                    Highcharts.chart(chId, {
                        chart: { type: 'pie' },
                        title: { text: chTitle },
                        subtitle: { text: chSubTitle },
                        accessibility: {
                            announceNewData: {
                                enabled: true
                            },
                            point: {
                                valueSuffix: '%'
                            }
                        },
                        plotOptions: {
                            series: {
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.name}: {point.y:.1f}%'
                                }
                            }
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                        },
                        credits: {
                            enabled: false
                        },
                        series: $.parseJSON(chSeries),
                        drilldown: {
                            series: $.parseJSON(chDDown)
                        }
                    });
                }
            } //.......HC_PieDDown

            function HC_LineSingInfo(_code) {
                var chId = 'IpbChart_chId' + _code,
                    chTitle = $('#IpbChart_chTitle' + _code).text(),
                    chSubTitle = $('#IpbChart_chSubTitle' + _code).text(),
                    chYAxiz = $('#IpbChart_chYAxiz' + _code).text(),
                    chXAxiz = $('#IpbChart_chXAxiz' + _code).text(),
                    chXStart = $('#IpbChart_chXStart' + _code).text(),
                    chSeries = $('#IpbChart_chSeries' + _code).text();

                if ($('#' + chId).length) {
                    Highcharts.chart(chId, {
                        title: { text: chTitle },
                        subtitle: { text: chSubTitle },
                        yAxis: {
                            title: { text: chYAxiz }
                        },
                        xAxis: {
                            accessibility: { rangeDescription: chXAxiz }
                        },
                        legend: {
                            layout: 'vertical',
                            align: 'right',
                            verticalAlign: 'middle'
                        },
                        plotOptions: {
                            series: {
                                label: {
                                    connectorAllowed: false
                                },
                                pointStart: parseInt(chXStart || '0')
                            }
                        },
                        credits: {
                            enabled: false
                        },
                        series: $.parseJSON(chSeries),
                        responsive: {
                            rules: [{
                                condition: {
                                    maxWidth: 500
                                },
                                chartOptions: {
                                    legend: {
                                        layout: 'horizontal',
                                        align: 'center',
                                        verticalAlign: 'bottom'
                                    }
                                }
                            }]
                        }
                    });
                }
            } //.......HC_LineSingInfo

            function HC_SpLineChart(_code) {
                var chId = 'IpbChart_chId' + _code,
                    chTitle = $('#IpbChart_chTitle' + _code).text(),
                    chSubTitle = $('#IpbChart_chSubTitle' + _code).text(),
                    chYAxiz = $('#IpbChart_chYAxiz' + _code).text(),
                    chXAxiz = $('#IpbChart_chXAxiz' + _code).text(),
                    chSeries = $('#IpbChart_chSeries' + _code).text();

                if ($('#' + chId).length) {
                    Highcharts.chart(chId, {
                        chart: { type: 'spline' },
                        title: { text: chTitle },
                        subtitle: { text: chSubTitle },
                        xAxis: {
                            categories: chXAxiz
                        },
                        yAxis: {
                            title: { text: chYAxiz },
                            labels: { formatter: function () { return this.value; } }
                        },
                        tooltip: { crosshairs: true, shared: true },
                        plotOptions: { spline: { marker: { radius: 4, lineColor: '#666666', lineWidth: 1}} },
                        credits: { enabled: false },
                        series: $.parseJSON(chSeries)
                    });
                }
            } //.......HC_SpLineInfo

            function C3_LineChart(_code) {
                var chId = 'IpbChart_chId' + _code,
                    chLabels = $('#IpbChart_chLabels' + _code).text(),
                    chValues = $('#IpbChart_chValues' + _code).text();

                if ($('#' + chId).length) {
                    var c3chart_spline = c3.generate({
                        bindto: '#' + chId,
                        data: {
                            json: $.parseJSON(chValues),
                            keys: {
                                x: 'label', // it's possible to specify 'x' when category axis
                                value: $.parseJSON(chLabels)
                            },
                            labels: true
                        },
                        axis: {
                            x: {
                                type: 'category',
                                tick: {
                                    rotate: 80,
                                    multiline: false
                                },
                                height: 140
                            }
                        },
                        color: {
                            pattern: ['#d62728', '#2ca02c', '#FB8C00', '#5E35B1', '#8c564b', '#9467bd', '#e377c2', '#7f7f7f', '#bcbd22', '#17becf']
                        }
                    });

                    $window.on('debouncedresize', function () {
                        c3chart_spline.resize();
                    });
                }
            } //.......C3_LineChart


            //...... Initialization
            var aCode = PCode.replace(/\s+/g, ',').replace(/,+/g, ',').replace(/^\,|\,$/gm, '').split(',');
            if ($.inArray("2", aCode) >= 0) HC_LineSingInfo('2');
            if ($.inArray("9", aCode) >= 0) HC_PieDDown('9');
            if ($.inArray("12", aCode) >= 0) HC_PieDDown('12');
            if ($.inArray("11", aCode) >= 0) C3_LineChart('11');
            if ($.inArray("14", aCode) >= 0) HC_SpLineChart('14');

        } // End Initialization
        IpbFnFirstBooting();