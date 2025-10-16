"use strict";
$(function () {
    $(".sr-select2").select2({
        width: '100%',
        placeholder: "-- Select --",
    });

    $(".sr-select2-multi").select2({
        width: '100%',
        placeholder: "-- Select --",
        multiple: true,
    });

    // Ensure no pre-selection when the dropdown is opened (for single selects)
    $('.sr-select2').on('select2:open', function () {
        if (!$(this).val()) {
            $(this).val([]).trigger('change');  // Reset the selection if no value is selected initially
        }

        $('.select2-results__options').css('white-space', 'nowrap');
        $('.select2-results__options').css('overflow-x', 'auto');
    });

    // Initialize select2 with checkboxes for multi-selection
    $('.sr-select2-multi-checkbox').select2({
        closeOnSelect: false,
        templateResult: formatCheckbox,
        multiple: true,
        placeholder: 'Select options',
        allowClear: true,
        width: '100%'  // Ensure it takes full width
    });

    function formatCheckbox(option) {
        if (!option.id) return option.text;
        let checked = $('.sr-select2-multi-checkbox').find('option[value="' + option.id + '"]').prop('selected');
        return $('<span><input type="checkbox" ' + (checked ? 'checked' : '') + ' /> ' + option.text + '</span>');
    }

    $(document).on('change', '.select-checkbox', function () {
        let option = $('.sr-select2-multi-checkbox option').filter((_, opt) => $(opt).text() === $(this).parent().text().trim());
        option.prop('selected', this.checked).trigger('change');
    });

    $('.sr-select2-multi-checkbox').on('select2:close', function () {
        $(this).trigger('change');
    });

    // Reset value to ensure no value is selected initially for single select
    $('.sr-select2').val(null).trigger('change');
});
